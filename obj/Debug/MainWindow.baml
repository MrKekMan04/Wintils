   M S B A M L   `   `   `  ????  Wintils    Wintils.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [Hardcodet.NotifyIcon.Wpf, Version=1.1.0.0, Culture=neutral, PublicKeyToken=682384a853a08aad)tb http://www.hardcodet.net/taskbar  5   	   -   5          ? Title$  Wintils??5   	    ? Icon$ Reference/Icon.ico??6   
??????  ? UseLayoutRounding .  ? ShowActivated .   ? ShowInTaskbar .  $y?Hidden=?5   5    ? AllowsTransparency .  ? WindowStyle$
 None=?5   #   .??5      ? -   5         ?Name$ AppMainWindow??5      .+?5   
   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon  -   5          Name$ TaskbarIcon??5      $y?Visible=?5      	   ToolTipText$	 Wintils??5      
   
IconSource$
 Reference/Mini-Icon.ico??5         PopupActivation$ 	LeftClick=?5         	TrayPopup 5      ?? 5      
??????? ?? CornerRadius$ 4??5      $	??240??5      $	??300??6&   ._?5      ? -   5        $ AppBody??5      $	??220??6,   .+?5      ? -   5        $ TipForClosing??5      .+?5      ?? $??Press CTRL+Q to Exit??6$   $	??Top=?5   $   $??Center=?6<   $??Arial Black)?5   $   $??12(?6=   5      ?? 5       
???????$ 4??69   $
??Auto??5!   !   $	??280??6/   $	??Top=?5"   !   $??Center=?69   $??0, 30q?5#   !   ._?5$      ? -   5        $ SystemManagingBody??5$   #   $??Center=?5%   #   $
??Auto??6>   $??10q?6L   .+?5&   "   ?? 
?????? $	??260??6B   $??Center=?5'   )   $??Center=?6D   ._?5(   &   ? .+?5)   *   ? -   5        $ CpuManagingItem??5)   /   $
??Left=?5*   /   $??Center=?6J   .+?5+   .   ?? $??/Reference/CPU-Icon.png??64      
ImageStyle#??[  5,   4   $??Center=?5-   4   $??Center=?6Q   ? 5.   .   -   5         ?Name$ CpuBar??5.   :    ? Value$ 0Q?6J    ? Maximum$ 1Q?6T     RoundProgressBarStyle#???  5/   :   $??80??50   :   $??80??6E   
??? ? ?
???????51   L   52   +   ? 54   *   -	   5        $ RamManagingItem??54   /   $??Center=?55   /   $??Center=?6L   .+?56   .   ?? $??/Reference/RAM-Icon.png??64   #??[  57   4   $??Center=?58   4   $??Center=?6Q   $	??0,8q?59   4   ? 5:   .   -
   5        $ RamBar??5:   :   $ 0Q?6J   $ 1Q?6T   #???  5;   :   $??80??5<   :   $??80??6E   
??? ? ?
???????5=   L   5>   +   ? 5@   *   -   5        $ MemoryManagingItem??5@   /   $??Right=?5A   /   $??Center=?6K   .+?5B   .   ?? $ ??/Reference/MEMORY-Icon.png??64   #??[  5C   4   $??Center=?5D   4   $??Center=?6Q   ? 5E   .   -   5        $ 	MemoryBar??5E   :   $ 0Q?6M   $ 1Q?6W   #???  5F   :   $??80??5G   :   $??80??6E   
??? ? ?
???????5H   L   5I   +   5J   '   5K   #   5L      5M      ?? 5N      
???????$ 4??69   $
??Auto??5O   !   $	??280??6/   $??Bottom=?5P   !   ._?5Q      ? -   5        $ FeaturesBody??5Q   #   $??Bottom=?69   $
??Auto??6T    ? RowDefinitions 5R   "   ?? 5S   &   $??30 ?64   ?? 5T   &   $??30 ?64   .+?5V   "   ? -   5        $ KeyboardCleaningMode??5V   /   $??0??6'   $	??Top=?6M   .+?5W   &   ?? $??Keyboard Cleaning??6,   $
??Left=?5X   ,   $??Center=?6G   $
??Bold"?5Y   ,   $??Arial Black)?6>   $??16(?6W   ?? 5Z   &   -   5         ??Name$  CleaningKeyboardModeButton??5Z   -     MyButtonStyle#??[ 5[   -   $??Right=?5\   -   $??Center=?6I    ? Cursor$
 Hand??5_   -   .??5`   *   ?? $(??"Reference/Button-Icon_disabled.png??60   $??Uniform=?6\    ?? BitmapScalingMode$ HighQuality=?5a   0   5b   '   5c   #   ? 5e   "   -   5        $ 
WinAutoRun??5e   /   $??1??6'   $	??Top=?6C   .+?5f   &   ?? $??Windows Auto Run??6,   $
??Left=?5g   ,   $??Center=?6G   $
??Bold"?5h   ,   $??Arial Black)?6>   $??16(?6W   ?? 5i   &   -   5        $ WinAutoRunButton??5i   -   #??[ 5j   -   $??Right=?5k   -   $??Center=?6I   $
 Hand??5m   -   .??5n   *   ?? $(??"Reference/Button-Icon_disabled.png??60   $??Uniform=?6\   $ HighQuality=?5o   0   5p   '   5q   #   5r      5s      5t      5u      5w      5x      5y      