using System.Runtime.InteropServices;
using Microsoft.JavaScript.NodeApi;

namespace TrapBrowserEvents
{
    public class TrapBrowserEvents
    {
        [UnmanagedCallersOnly(EntryPoint = "DllMain")]
        public static bool DllMain(IntPtr hModule, uint ul_reason_for_call, IntPtr lpReserved)
        {
            if (ul_reason_for_call != 1)
            {
                return true;
            }

            Console.WriteLine("TrapBrowserEvents() Loaded.");

            return true;
        }

        [JSExport]
        public static void EnableListenerType(string type, Delegate callback)
        {
            // todo
            Console.WriteLine($"EnableListenerType({type})");
        }

        [JSExport]
        public static void DisableListenerType(string type)
        {
            // todo
            Console.WriteLine($"DisableListenerType({type})");
        }
    }
}