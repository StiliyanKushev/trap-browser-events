using static TrapBrowserEvents.HookManager;

namespace TrapBrowserEvents.listeners
{
    public class ContextMenuOpened
    {
        public static void InitializeHookHandler()
        {
            hookEventHandler += ContextMenuOpened_hookEventHandler;
        }

        private static void ContextMenuOpened_hookEventHandler(WinEvents eventType, IntPtr hwnd)
        {
            if(!TrapBrowserEvents.eventDispatchers.ContainsKey("contextMenuClicked"))
            {
                return;
            }

            if(eventType != WinEvents.EVENT_SYSTEM_FOREGROUND)
            {
                return;
            }

            try
            {
                TrapBrowserEvents.eventDispatchers["contextMenuClicked"].Start();
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
