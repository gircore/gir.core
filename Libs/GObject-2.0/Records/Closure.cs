using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial record Closure : IDisposable
    {
        private readonly ClosureMarshalCallHandler _closureMarshalCallHandler;
        private readonly Native.ClosureSafeHandle _handle;

        public IntPtr Handle => _handle.IsInvalid ? IntPtr.Zero : _handle.DangerousGetHandle();

        internal Closure(ClosureMarshal action)
        {
            _closureMarshalCallHandler = new ClosureMarshalCallHandler(action);
            _handle = Native.Methods.NewSimple((uint) Marshal.SizeOf(typeof(Closure)), IntPtr.Zero);

            Native.Methods.Ref(_handle);
            Native.Methods.Sink(_handle);
            Native.Methods.SetMarshal(_handle, _closureMarshalCallHandler.NativeCallback);
        }

        public void Dispose()
        {
            _closureMarshalCallHandler.Dispose();
            _handle.Dispose();
        }
    }
}
