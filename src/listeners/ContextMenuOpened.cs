using static TrapBrowserEvents.HookManager;
using static TrapBrowserEvents.WindowProperties;
using System.Linq;

namespace TrapBrowserEvents.listeners
{
    public class ContextMenuOpened
    {
        public static void InitializeHookHandler()
        {
            hookEventHandler += (WinEvents eventType, IntPtr hWnd) => {
                if (!TrapBrowserEvents.eventDispatchers.ContainsKey(EventTypes.CONTEXT_MENU_CLICKED))
                {
                    return;
                }

                if (eventType != WinEvents.EVENT_OBJECT_CREATE)
                {
                    return;
                }

                string windowTitle = GetWindowTitle(hWnd);

                if(windowTitle.Trim().Length > 0)
                {
                    return;
                }

                var windowInfo = GetWindowInfo(hWnd);

                if ((windowInfo.dwStyle & WindowStyles.WS_CLIPCHILDREN) == 0)
                {
                    return;
                }

                if ((windowInfo.dwStyle & WindowStyles.WS_CLIPSIBLINGS) == 0)
                {
                    return;
                }

                if ((windowInfo.dwStyle & WindowStyles.WS_POPUP) == 0)
                {
                    return;
                }

                if ((windowInfo.dwStyle & WindowStyles.WS_POPUPWINDOW) == 0)
                {
                    return;
                }

                if ((windowInfo.dwExStyle & WindowStyles.WS_EX_NOACTIVATE) == 0)
                {
                    return;
                }

                if ((windowInfo.dwExStyle & WindowStyles.WS_EX_PALETTEWINDOW) == 0)
                {
                    return;
                }

                if ((windowInfo.dwExStyle & WindowStyles.WS_EX_TOOLWINDOW) == 0)
                {
                    return;
                }

                if ((windowInfo.dwExStyle & WindowStyles.WS_EX_TOPMOST) == 0)
                {
                    return;
                }

                // Unknown reason, but this is the class name chromium based 
                // applications seem to use for context menu popups.
                const int CONTEXT_MENU_CLASS_ATOM = 49534;

                if (windowInfo.atomWindowType != CONTEXT_MENU_CLASS_ATOM)
                {
                    return;
                }

                string processName = GetProcessName(GetProcessId(hWnd));

                // finally, let's check if this is comming from a browser.
                // unlikely, but it might be comming from an electron instance or such.
                if (!TrapBrowserEvents.BrowserProcessNames.Contains(processName))
                {
                    return;
                }

                TrapBrowserEvents.eventDispatchers[EventTypes.CONTEXT_MENU_CLICKED].Set();
            };
        }
    }
}
