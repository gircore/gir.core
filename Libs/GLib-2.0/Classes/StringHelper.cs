using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class StringHelper
    {
        public static string? ToNullableAnsiStringAndFree(IntPtr ptr)
        {
            var resultString = Marshal.PtrToStringAnsi(ptr);
            Functions.Native.Free(ptr);
            return resultString;
        }

        public static string ToAnsiStringAndFree(IntPtr ptr)
            => ToNullableAnsiStringAndFree(ptr) ?? string.Empty;
    }
}
