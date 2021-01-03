using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class MarshalHelper
    {
        public static K ToPtrAndFree<T, K>(T structure, Func<IntPtr, K> action)  where T : struct
        {
            K result;
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(structure));
            try
            {
                Marshal.StructureToPtr(structure, ptr, true);
                result = action(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }

            return result;
        }
        
        public static void ToPtrAndFree<T>(T structure, Action<IntPtr> action) where T : struct
        {
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(structure));
            try
            {
                Marshal.StructureToPtr(structure, ptr, true);
                action(ptr);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
    }
}
