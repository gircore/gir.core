using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class IntPtrExtension
    {
        public static string ToAnsiStringAndFree(this IntPtr ptr)
        {
            var resultString = Marshal.PtrToStringAnsi(ptr);
            Global.Native.free(ptr);
            return resultString ?? string.Empty;
        }
    }
}
