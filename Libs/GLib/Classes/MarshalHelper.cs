using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public class MarshalHelper
    {
        public static K Execute<T, K>(T s, Func<IntPtr, K> action)  where T : struct
        {
            K result;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(s));
            try
            {
                Marshal.StructureToPtr(s, ptr, true);
                result = action(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return result;
        }
        
        public static void Execute<T>(T s, Action<IntPtr> action) where T : struct
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(s));
            try
            {
                Marshal.StructureToPtr(s, ptr, true);
                action(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
