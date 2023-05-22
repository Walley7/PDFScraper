using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;



namespace PDFScrape.Utility {

    public class UWindows {
        //================================================================================
        public const int                        GWL_EXSTYLE = -20;

        public const int                        WS_EX_LAYERED = 0x80000;
        public const int                        WS_EX_TRANSPARENT = 0x20;

        public const int                        LWA_ALPHA = 0x2;
     

        // WINDOWS ================================================================================
        //--------------------------------------------------------------------------------
        [DllImport("user32.dll", SetLastError = true)] public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")] public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll")] public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
    }

}
