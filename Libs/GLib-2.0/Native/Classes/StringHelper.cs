using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Native
{
    public static class StringHelper
    {
        /// <summary>
        /// Interpretes the given ptr as a string.
        /// </summary>
        /// <returns>a managed version of the string.</returns>
        /// <remarks>Use this method if the ptr should not be freed by the marshaller.</remarks>
        public static string ToAutoString(IntPtr ptr)
            => Marshal.PtrToStringAuto(ptr) ?? string.Empty;

        /// <summary>
        /// Interpetes the given ptr as a null terminated string array.
        /// </summary>
        /// <returns>A managed version of the string array.</returns>
        /// <remarks>Use this method if the ptr should not be freed by the marshaller.</remarks>
        public static string[] ToStringArray(IntPtr ptr)
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
                
                var str = ToAutoString(currentPointer);
                data.Add(str);
                
                offset++;
            }
            
            return data.ToArray();
        }
    }
}
