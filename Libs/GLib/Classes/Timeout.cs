using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Timeout
    {
        // FIXME: FuncData doesn't work in practice. A much better
        // solution is to simply generate managed and unmanaged signature
        // delegates appropriately, and provide helpers for marshalling
        // between them (i.e. like TSignalArgs).
        
        public static uint Add(uint interval, Func<bool> function)
        {
            // TODO: This is broken (we cannot marshal FuncData)
            var data = new FuncData(function);
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(FuncData)));
            Marshal.StructureToPtr<FuncData>(data, ptr, false);
            return Global.Native.timeout_add(interval, TimeoutSourceFunction, ptr);
        }

        private static bool TimeoutSourceFunction(IntPtr data)
        {
            FuncData funcData = Marshal.PtrToStructure<FuncData>(data);
            var result = funcData.managedFunction.Invoke();

            if (result == false)
            {
                // When result is false, this will never be called again
                // and GLib will free the underlying memory. Take this
                // opportunity to free FuncData
                Marshal.FreeHGlobal(data);
            }

            return result;
        }
        
        internal struct FuncData
        {
            internal readonly Func<bool> managedFunction;

            internal FuncData(Func<bool> function)
                => this.managedFunction = function;
        }
    }
}
