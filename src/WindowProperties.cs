
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace TrapBrowserEvents
{
    public class WindowProperties
    {

        //////////////////////////////////////////////
        //            Get the Window Title          //
        //////////////////////////////////////////////

        public static string GetWindowTitle(IntPtr hWnd)
        {
            var length = GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            GetWindowText(hWnd, title, length);
            return title.ToString();
        }

        /////////////////////////////////////////////////////////
        //            Get the Window Styles (Extended)         //
        /////////////////////////////////////////////////////////

        private const int GWL_EXSTYLE = -20;
        public static IntPtr GetWindowStylesEx(IntPtr hWnd) 
        {
            return GetWindowLongPtr(hWnd, GWL_EXSTYLE);
        }

        ////////////////////////////////////////////////////////////
        //            Get the Application Instance Handle         //
        ////////////////////////////////////////////////////////////

        private const int GWLP_HINSTANCE = -6;
        public static IntPtr GetApplicationHandle(IntPtr hWnd)
        {
            return GetWindowLongPtr(hWnd, GWLP_HINSTANCE);
        }

        //////////////////////////////////////////////////////
        //            Get the Parent Window Handle          //
        //////////////////////////////////////////////////////

        private const int GWLP_HWNDPARENT = -8;
        public static IntPtr GetParentWindowHandle(IntPtr hWnd)
        {
            return GetWindowLongPtr(hWnd, GWLP_HWNDPARENT);
        }

        ///////////////////////////////////////////////
        //            Get the Window Styles          //
        ///////////////////////////////////////////////

        private const int GWL_STYLE = -8;
        public static IntPtr GetWindowStyles(IntPtr hWnd)
        {
            return GetWindowLongPtr(hWnd, GWL_STYLE);
        }

        ////////////////////////////////////////////////////
        //            Get the Window Coordinates          //
        ////////////////////////////////////////////////////

        public static RECT GetWindowRect(IntPtr hWnd)
        {
            RECT rct;
            GetWindowRect(hWnd, out rct);
            return rct;
        }

        /////////////////////////////////////////////////////////
        //            Get the Client Area Coordinates          //
        /////////////////////////////////////////////////////////

        public static RECT GetClientRect(IntPtr hWnd)
        {
            RECT rct;
            GetClientRect(hWnd, out rct);
            return rct;
        }

        //////////////////////////////////////////////////////
        //            Get Every Window Information          //
        //////////////////////////////////////////////////////

        public static WINDOWINFO GetWindowInfo(IntPtr hWnd)
        {
            WINDOWINFO info = new WINDOWINFO();
            info.cbSize = (uint)Marshal.SizeOf(info);
            GetWindowInfo(hWnd, ref info);
            return info;
        }

        ///////////////////////////////////////////////
        //            Get Window Class Name          //
        ///////////////////////////////////////////////

        public static string GetWindowClassName(IntPtr atomWindowType)
        {
            // Assuming the class name will fit in 256 characters
            var className = new StringBuilder(256);
            GetClassName(atomWindowType, className, className.Length);
            return className.ToString();
        }

        //////////////////////////////////////////////////////
        //            Get Window Process/Thread ID          //
        //////////////////////////////////////////////////////

        public static uint GetProcessId(IntPtr hWnd)
        {
            GetWindowThreadProcessId(hWnd, out uint processID);
            return processID;
        }
        public static uint GetThreadId(IntPtr hWnd)
        {
            uint threadID = GetWindowThreadProcessId(hWnd, IntPtr.Zero);
            return threadID;
        }

        //////////////////////////////////////////
        //            Get Process Name          //
        //////////////////////////////////////////

        const uint PROCESS_QUERY_INFORMATION = 0x0400;
        const uint PROCESS_VM_READ = 0x0010;

        public static string GetProcessName(uint processId)
        {
            return GetProcessName((int)processId);
        }

        public static string GetProcessName(int processId)
        {
            IntPtr hProcess = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, processId);
            var processName = new StringBuilder(260);
            GetModuleBaseName(hProcess, IntPtr.Zero, processName, 260);
            CloseHandle(hProcess);
            return processName.ToString().Trim().ToLower();
        }

        //////////////////////////////////
        //            Pinvokes          //
        //////////////////////////////////

        public abstract class WindowStyles
        {
            public const uint WS_OVERLAPPED = 0x00000000;
            public const uint WS_POPUP = 0x80000000;
            public const uint WS_CHILD = 0x40000000;
            public const uint WS_MINIMIZE = 0x20000000;
            public const uint WS_VISIBLE = 0x10000000;
            public const uint WS_DISABLED = 0x08000000;
            public const uint WS_CLIPSIBLINGS = 0x04000000;
            public const uint WS_CLIPCHILDREN = 0x02000000;
            public const uint WS_MAXIMIZE = 0x01000000;
            public const uint WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
            public const uint WS_BORDER = 0x00800000;
            public const uint WS_DLGFRAME = 0x00400000;
            public const uint WS_VSCROLL = 0x00200000;
            public const uint WS_HSCROLL = 0x00100000;
            public const uint WS_SYSMENU = 0x00080000;
            public const uint WS_THICKFRAME = 0x00040000;
            public const uint WS_GROUP = 0x00020000;
            public const uint WS_TABSTOP = 0x00010000;

            public const uint WS_MINIMIZEBOX = 0x00020000;
            public const uint WS_MAXIMIZEBOX = 0x00010000;

            public const uint WS_TILED = WS_OVERLAPPED;
            public const uint WS_ICONIC = WS_MINIMIZE;
            public const uint WS_SIZEBOX = WS_THICKFRAME;
            public const uint WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

            // Common Window Styles

            public const uint WS_OVERLAPPEDWINDOW =
                (WS_OVERLAPPED |
                  WS_CAPTION |
                  WS_SYSMENU |
                  WS_THICKFRAME |
                  WS_MINIMIZEBOX |
                  WS_MAXIMIZEBOX);

            public const uint WS_POPUPWINDOW =
                (WS_POPUP |
                  WS_BORDER |
                  WS_SYSMENU);

            public const uint WS_CHILDWINDOW = WS_CHILD;

            //Extended Window Styles

            public const uint WS_EX_DLGMODALFRAME = 0x00000001;
            public const uint WS_EX_NOPARENTNOTIFY = 0x00000004;
            public const uint WS_EX_TOPMOST = 0x00000008;
            public const uint WS_EX_ACCEPTFILES = 0x00000010;
            public const uint WS_EX_TRANSPARENT = 0x00000020;

            //#if(WINVER >= 0x0400)
            public const uint WS_EX_MDICHILD = 0x00000040;
            public const uint WS_EX_TOOLWINDOW = 0x00000080;
            public const uint WS_EX_WINDOWEDGE = 0x00000100;
            public const uint WS_EX_CLIENTEDGE = 0x00000200;
            public const uint WS_EX_CONTEXTHELP = 0x00000400;

            public const uint WS_EX_RIGHT = 0x00001000;
            public const uint WS_EX_LEFT = 0x00000000;
            public const uint WS_EX_RTLREADING = 0x00002000;
            public const uint WS_EX_LTRREADING = 0x00000000;
            public const uint WS_EX_LEFTSCROLLBAR = 0x00004000;
            public const uint WS_EX_RIGHTSCROLLBAR = 0x00000000;

            public const uint WS_EX_CONTROLPARENT = 0x00010000;
            public const uint WS_EX_STATICEDGE = 0x00020000;
            public const uint WS_EX_APPWINDOW = 0x00040000;

            public const uint WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
            public const uint WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);
            //#endif /* WINVER >= 0x0400 */

            //#if(_WIN32_WINNT >= 0x0500)
            public const uint WS_EX_LAYERED = 0x00080000;
            //#endif /* _WIN32_WINNT >= 0x0500 */

            //#if(WINVER >= 0x0500)
            public const uint WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children
            public const uint WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring
                                                            //#endif /* WINVER >= 0x0500 */

            //#if(_WIN32_WINNT >= 0x0500)
            public const uint WS_EX_COMPOSITED = 0x02000000;
            public const uint WS_EX_NOACTIVATE = 0x08000000;
            //#endif /* _WIN32_WINNT >= 0x0500 */
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWINFO
        {
            public uint cbSize;
            public RECT rcWindow;
            public RECT rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
            public WINDOWINFO(Boolean? filler) : this()   // Allows automatic initialization of "cbSize" with "new WINDOWINFO(null/true/false)".
            {
                cbSize = (UInt32)(Marshal.SizeOf(typeof(WINDOWINFO)));
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);

        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("psapi.dll")]
        static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, uint nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        [SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
    }
}
