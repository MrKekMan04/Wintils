using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Wintils.Helpers;

namespace Wintils
{
    public partial class MainWindow
    {
        private delegate void CheckingSystemStatistics();

        private bool _isWinAutoRun;

        private readonly PerformanceCounter _cpu;
        private readonly PerformanceCounter _ram;
        private readonly DriveInfo _driveInfo;
        private long _lastCheck;
        private bool _isCleaningMode;
        private readonly KeyboardHookHelper _keyboardHookHelper;

        private readonly CheckingSystemStatistics _checkSystemStatistics;

        private static readonly DateTime StartCountdown = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetPhysicallyInstalledSystemMemory(out long totalMemoryInKilobytes);

        public MainWindow()
        {
            InitializeComponent();

            _checkSystemStatistics = CheckStatistics;

            _isWinAutoRun = Convert.ToBoolean(RegistryHelper.GetCurrentUserRegistryValue("isWinAutoRun", true));
            WinAutoRunButton.Content = GetButtonImage(_isWinAutoRun);
            RegistryHelper.SetAutorunValue(_isWinAutoRun);

            _keyboardHookHelper = new KeyboardHookHelper(this);
            _keyboardHookHelper.SetHook();

            _cpu = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _ram = new PerformanceCounter("Memory", "Available MBytes");
            _driveInfo = new DriveInfo(Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)));
        }

        private void CleaningKeyboardModeButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).IsPressed)
            {
                _isCleaningMode = !_isCleaningMode;
                CleaningKeyboardModeButton.Content = GetButtonImage(_isCleaningMode);
            }
        }

        private void WinAutoRunButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (((Button)sender).IsPressed)
            {
                _isWinAutoRun = !_isWinAutoRun;
                WinAutoRunButton.Content = GetButtonImage(_isWinAutoRun);
                RegistryHelper.SetCurrentUserRegistryValue("isWinAutoRun", _isWinAutoRun);
                RegistryHelper.SetAutorunValue(_isWinAutoRun);
            }
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e) => _keyboardHookHelper.Unhook();

        private void TaskbarIcon_OnTrayPopupOpen(object sender, RoutedEventArgs e) =>
            Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, _checkSystemStatistics);

        private void CheckStatistics()
        {
            var nowMillisecond = CurrentTimeMillis();
            if (nowMillisecond - _lastCheck >= 1000)
            {
                CpuBar.SetPercent(_cpu.NextValue() / 100);

                GetPhysicallyInstalledSystemMemory(out var totalMemoryKb);
                var totalMemoryMb = totalMemoryKb / 1024.0;
                RamBar.SetPercent((totalMemoryMb - _ram.NextValue()) / totalMemoryMb);

                var totalMemory = CastMemory(_driveInfo.TotalSize);
                var usedMemory = CastMemory(_driveInfo.TotalSize - _driveInfo.AvailableFreeSpace, totalMemory.Item2);
                MemoryBar.SetPercent(usedMemory.Item1 / totalMemory.Item1);

                _lastCheck = nowMillisecond;
            }

            if (TaskbarIcon.TrayPopup.IsVisible)
                Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, _checkSystemStatistics);
            else
                ClearBarValues();
        }

        private void ClearBarValues()
        {
            CpuBar.SetPercent(0);
            RamBar.SetPercent(0);
            MemoryBar.SetPercent(0);
        }

        private static Tuple<double, MemoryType> CastMemory(long memoryInBytes, MemoryType castToType = MemoryType.None)
        {
            if ((memoryInBytes < 1024 && castToType == MemoryType.None) || castToType == MemoryType.B)
                return new Tuple<double, MemoryType>(memoryInBytes, MemoryType.B);
            if ((memoryInBytes < 1024 * 1024 && castToType == MemoryType.None) || castToType == MemoryType.Kb)
                return new Tuple<double, MemoryType>(memoryInBytes / 1024.0, MemoryType.Kb);
            if ((memoryInBytes < 1024 * 1024 * 1024 && castToType == MemoryType.None) || castToType == MemoryType.Mb)
                return new Tuple<double, MemoryType>(memoryInBytes / 1024.0 / 1024, MemoryType.Mb);
            if ((memoryInBytes < Math.Pow(1024, 4) && castToType == MemoryType.None) || castToType == MemoryType.Gb)
                return new Tuple<double, MemoryType>(memoryInBytes / 1024.0 / 1024 / 1024, MemoryType.Gb);
            return (memoryInBytes < Math.Pow(1024, 5) && castToType == MemoryType.None)
                   || castToType == MemoryType.Tb || castToType == MemoryType.Pb
                ? castToType == MemoryType.Tb
                    ? new Tuple<double, MemoryType>(memoryInBytes / Math.Pow(1024, 4), MemoryType.Tb)
                    : new Tuple<double, MemoryType>(memoryInBytes / Math.Pow(1024, 5), MemoryType.Pb)
                : new Tuple<double, MemoryType>(memoryInBytes / Math.Pow(1024, 5), MemoryType.Pb);
        }

        public bool GetIsCleaningMode() => _isCleaningMode;

        private Image GetButtonImage(bool isButtonActive) => new Image
        {
            Stretch = Stretch.Uniform,
            Source = (ImageSource)FindResource(isButtonActive
                ? "EnabledButtonSource"
                : "DisabledButtonSource"),
            Style = (Style)FindResource("HighQualityRender")
        };

        private static long CurrentTimeMillis() => (long)(DateTime.UtcNow - StartCountdown).TotalMilliseconds;

        private enum MemoryType
        {
            None,
            B,
            Kb,
            Mb,
            Gb,
            Tb,
            Pb
        }
    }

    public static class ProgressBarExtensions
    {
        public static void SetPercent(this ProgressBar bar, double value)
        {
            bar.BeginAnimation(RangeBase.ValueProperty,
                new DoubleAnimation(bar.Value, value, new Duration(TimeSpan.FromSeconds(0.5))));
        }
    }
}