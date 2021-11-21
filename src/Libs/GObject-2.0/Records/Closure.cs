using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject
{
    //TODO: This should be in the native namespace. It does not belong into the managed api. We have events to handle this.
    public partial class Closure : IDisposable
    {
        // A call handler keeps the delegate alive for the
        // lifetime of the call handler. As we save it as a
        // field here, the delegate will match this class' lifetime.
        private readonly ClosureMarshalHandler _closureMarshalCallHandler;

        internal Closure(ClosureMarshal action)
        {
            _closureMarshalCallHandler = new ClosureMarshalHandler(action);
            _handle = Internal.Closure.Methods.NewSimple((uint) Marshal.SizeOf<GObject.Internal.Closure.Struct>(), IntPtr.Zero);

            Debug.WriteLine($"Instantiating Closure: Address {_handle.DangerousGetHandle()}.");

            Internal.Closure.Methods.Ref(_handle);
            Internal.Closure.Methods.Sink(_handle);
            Internal.Closure.Methods.SetMarshal(_handle, _closureMarshalCallHandler.NativeCallback);
        }

        public void Dispose()
        {
            Debug.WriteLine($"Disposing Closure: Address {_handle.DangerousGetHandle()}.");

            _closureMarshalCallHandler.Dispose();
            _handle.Dispose();
        }
    }
}
