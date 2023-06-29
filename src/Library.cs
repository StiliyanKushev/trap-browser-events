using System;
using Microsoft.JavaScript.NodeApi;

namespace TrapBrowserEvents
{
    public class TrapBrowserEvents
    {
        // holds all dispatch functions for each event type. When called, it notifies the JS side.
        public static Dictionary<string, Task> eventDispatchers = new Dictionary<string, Task>();

        [JSExport]
        public static Task EnableListenerType(string type)
        {
            var dispatch = new AsyncTask(() => { });

            if(!eventDispatchers.ContainsKey(type))
            {
                eventDispatchers.Add(type, dispatch);
            }

            if (!isListening)
            {
                BeginListening();
            }

            return new Task(() => {
                dispatch.Wait();
                return dispatch.Result;
            });
        }

        [JSExport]
        public static void DisableListenerType(string type)
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