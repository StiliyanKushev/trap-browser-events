using TrapBrowserEvents.listeners;

namespace TrapBrowserEvents.src
{
    public class Listeners
    {
        // initialize all event listners here
        public static void InitializeHookEventListeners()
        {
            ContextMenuOpened.InitializeHookHandler();
        }
    }
}
