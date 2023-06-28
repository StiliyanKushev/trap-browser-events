using System.Runtime.InteropServices;
using Microsoft.JavaScript.NodeApi;

public class TrapBrowserEvents
{
    [UnmanagedCallersOnly(EntryPoint = "DllMain")]
    public static bool DllMain(IntPtr hModule, uint ul_reason_for_call, IntPtr lpReserved)
    {
        if(ul_reason_for_call != 1)
        {
            return true;
        }

        Console.WriteLine("Hello World");

        return true;
    }
}