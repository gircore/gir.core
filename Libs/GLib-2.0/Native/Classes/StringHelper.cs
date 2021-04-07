using System;
using System.Runtime.InteropServices;

namespace GLib.Native
{
    public static class StringHelper
    {
        public static string? ToNullableAnsiStringAndFree(IntPtr ptr)
        {
            var resultString = Marshal.PtrToStringAnsi(ptr);
            Functions.Free(ptr);
            return resultString;
        }

        public static string ToAnsiStringAndFree(IntPtr ptr)
            => ToNullableAnsiStringAndFree(ptr) ?? string.Empty;

        /// <summary>
        /// Use this method if the ptr should not be freed by the marshaller.
        /// </summary>
        public static string ToAnsiString(IntPtr ptr)
            => Marshal.PtrToStringAuto(ptr) ?? string.Empty;
    }
}
