using System;
using System.Runtime.InteropServices;

namespace ice_cast_win
{
    public static class NativeMethods
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        public const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        public const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
    }
}