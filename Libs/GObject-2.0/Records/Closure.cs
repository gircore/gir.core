using System;
using System.Runtime.InteropServices;

namespace GObject
{
    //TODO: This should be in the native namespace. It does not belong into the managed api. We have events to handle this.
    public partial class Closure : IDisposable
    {
        private readonly ClosureMarshalCallHandlerWorkaround _closureMarshalCallHandler;

        internal Closure(Action action) //TODO: Restore: ClosureMarshal action)
        {
            _closureMarshalCallHandler = new ClosureMarshalCallHandlerWorkaround(action);
            _handle = Native.Closure.Methods.NewSimple((uint) Marshal.SizeOf(typeof(GObject.Native.Closure.Struct)), IntPtr.Zero);

            Native.Closure.Methods.Ref(_handle);
            Native.Closure.Methods.Sink(_handle);
            Native.Closure.Methods.SetMarshal(_handle, _closureMarshalCallHandler.NativeCallback);
        }

        public void Dispose()
        {
            _closureMarshalCallHandler.Dispose();
            _handle.Dispose();
        }
    }
}
