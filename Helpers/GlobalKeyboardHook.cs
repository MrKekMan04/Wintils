using System;
using System.Runtime.InteropServices;

namespace Wintils.Helpers
{
    internal class KeyboardHookHelper
    {
        private const int WH_KEYBOARD_LL = 13;
        private LowLevelKeyboardProcDelegate _mCallback;
        private IntPtr _mHHook;

        private readonly MainWindow _mainWindow;

        public KeyboardHookHelper(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProcDelegate lpfn,
            IntPtr hMod,
            int dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr LowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                var khs = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                if (_mainWindow.GetIsCleaningMode())
                    return new IntPtr(1);
                if (khs.VirtualKeyCode == 81 && wParam.ToInt32() == 256 && khs.ScanCode == 16)
                    if (_mainWindow.TaskbarIcon.TrayPopup.IsVisible)
                        _mainWindow.Close();
            }

            return CallNextHookEx(_mHHook, nCode, wParam, lParam);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            public readonly int VirtualKeyCode;

            public readonly int ScanCode;

            public readonly int Flags;

            public readonly int Time;

            public readonly IntPtr ExtraInfo;
        }

        private delegate IntPtr LowLevelKeyboardProcDelegate(int nCode, IntPtr wParam, IntPtr lParam);

        public void SetHook()
        {
            _mCallback = LowLevelKeyboardHookProc;

            _mHHook = SetWindowsHookEx(WH_KEYBOARD_LL, _mCallback, GetModuleHandle(IntPtr.Zero), 0);
        }

        public void Unhook() => UnhookWindowsHookEx(_mHHook);
    }
}