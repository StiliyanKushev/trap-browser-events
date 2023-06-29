using Microsoft.JavaScript.NodeApi;

namespace TrapBrowserEvents
{
    public enum EventTypes
    {
        CONTEXT_MENU_CLICKED,
        EXTENSION_MENU_CLICKED
    }

    public class TrapBrowserEvents
    {
        // ideally this should contain all chromium based browsers
        public static List<string> BrowserProcessNames = new List<string>();

        [JSExport]
        public static void TargetProcessName(string processName)
        {
            if(!BrowserProcessNames.Contains(processName)) 
            { 
                BrowserProcessNames.Add(processName);
            }
        }

        [JSExport]
        public static void ReleaseProcessName(string processName)
        {
            if (BrowserProcessNames.Contains(processName))
            {
                BrowserProcessNames.Remove(processName);
            }
        }

        // holds all dispatch functions for each event type. When called, it notifies the JS side.
        public static Dictionary<EventTypes, ManualResetEvent> eventDispatchers = new Dictionary<EventTypes, ManualResetEvent>();

        [JSExport]
        public static async Task EnableListenerType(EventTypes type)
        {
            if (!eventDispatchers.ContainsKey(type))
            {
                eventDispatchers.Add(type, new ManualResetEvent(false));
            }

            if (!isListening)
            {
                BeginListening();
            }

            await Task.Run(() => { 
                eventDispatchers[type].WaitOne();
                eventDispatchers[type].Reset();
            });
        }

        [JSExport]
        public static void DisableListenerType(EventTypes type)
        {
            if(!eventDispatchers.ContainsKey(type)) {
                Console.WriteLine($"Unable to disable ({type}) because it's not enabled.");
                return;
            }

            if(eventDispatchers.Count == 0 && isListening)
            {
                StopListening();
            }

            // remove the dispatch event function
            eventDispatchers.Remove(type);
            Console.WriteLine($"Disabled listener type ({type})");
        }

        static bool isListening = false;

        public static void BeginListening()
        {
            isListening = true;
            HookManager.SubscribeToWindowEvents();
        }

        public static void StopListening()
        {
            isListening = false;
            HookManager.UnsubscribeToWindowEvents();
        }
    }
}