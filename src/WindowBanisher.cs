using System.Runtime.InteropServices;

namespace TrapBrowserEvents
{
    public class WindowBanisher
    {
        public static void BanishWindow(IntPtr hWnd)
        {
            MoveWindow(hWnd, 9999, 9999, 1, 1, true);
            for(var i = 0; i < 1000; i++)
            {
                SendKey(hWnd, 0x1B);
            }
        }

        const int WM_KEYDOWN = 0x100;
        const int WM_KEYUP = 0x101;

        public static void SendKey(IntPtr hWnd, int key)
        {
            PostMessage(hWnd, WM_KEYDOWN, key, 0);
            PostMessage(hWnd, WM_KEYUP, key, 0);
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }
}
