using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial record Closure : IDisposable
    {
        private readonly ClosureMarshalCallHandler _closureMarshalCallHandler;
        private readonly Native.Closure.Handle _handle;

        public Native.Closure.Handle? Handle => _handle.IsInvalid ? null : _handle;

        internal Closure(ClosureMarshal action)
        {
            _closureMarshalCallHandler = new ClosureMarshalCallHandler(action);
            _handle = Native.Closure.Methods.NewSimple((uint) Marshal.SizeOf(typeof(Closure)), IntPtr.Zero);

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
