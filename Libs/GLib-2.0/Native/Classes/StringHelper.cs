using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Native
{
    public static class StringHelper
    {
        /// <summary>
        /// Interprets the given ptr as a non nullable string.
        /// </summary>
        /// <returns>a managed version of the string.</returns>
        /// <remarks>Use this method if the ptr should not be freed by the marshaller.</remarks>
        public static string ToStringUTF8(IntPtr ptr)
            => Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
        
        /// <summary>
        /// Interprets the given ptr as a nullable string.
        /// </summary>
        /// <returns>a managed version of the string.</returns>
        /// <remarks>Use this method if the ptr should not be freed by the marshaller.</remarks>
        public static string? ToNullableStringUTF8(IntPtr ptr)
            => Marshal.PtrToStringUTF8(ptr);

        /// <summary>
        /// Interprets the given ptr as a null terminated string array.
        /// </summary>
        /// <returns>A managed version of the string array.</returns>
        /// <remarks>Use this method if the ptr should not be freed by the marshaller.</remarks>
        public static string[] ToStringUTF8Array(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return System.Array.Empty<string>();

            var data = new List<string>();
            var offset = 0;
            while (true)
            {
                var currentPointer = Marshal.ReadIntPtr(ptr, offset * IntPtr.Size);

                if (currentPointer == IntPtr.Zero)
                    break;
                
                var str = ToStringUTF8(currentPointer);
                data.Add(str);
                
                offset++;
            }
            
            return data.ToArray();
        }
    }
}
