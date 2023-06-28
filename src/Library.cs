using Microsoft.JavaScript.NodeApi;

namespace TrapBrowserEvents
{
    public class TrapBrowserEvents
    {
        [JSExport]
        public static void EnableListenerType(string type, JSValue dispatchFunction)
        {
            // todo
            Console.WriteLine($"EnableListenerType({type})");
            dispatchFunction.Call();
        }

        [JSExport]
        public static void DisableListenerType(string type)
        {
            // todo
            Console.WriteLine($"DisableListenerType({type})");
        }
    }
}