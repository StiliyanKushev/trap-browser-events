using System.Runtime.InteropServices;

namespace TrapBrowserEvents
{
    public class MainDLL
    {
        public static IntPtr currentHModule = 0;

        [UnmanagedCallersOnly(EntryPoint = "DllMain")]
        public static bool DllMain(IntPtr hModule, uint ul_reason_for_call, IntPtr lpReserved)
        {
            switch (ul_reason_for_call)
            {
                case 1:
                    currentHModule = hModule;
                    break;
                default:
                    break;
            }
            return true;
        }
    }
}
