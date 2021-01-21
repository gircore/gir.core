using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        protected internal delegate void ActionRefValues(ref Value[] items);

        private class ClosureHelper : IDisposable
        {
            #region Fields
            
            // We need to store a reference to MarshalCallback to
            // prevent the delegate from being collected by the GC
            private readonly ClosureMarshal? _marshalCallback;
            private readonly ActionRefValues? _callback;
            private readonly Closure.ClosureSafeHandle _safeHandle; 
            
            #endregion
            
            #region Properties

            public IntPtr Handle => _safeHandle?.DangerousGetHandle() ?? IntPtr.Zero;

            #endregion
            
            #region Constructors
            
            public ClosureHelper(ActionRefValues action)
            {
                _callback = action;
                _marshalCallback = MarshalCallback;

                var handle = Closure.Native.new_simple((uint) Marshal.SizeOf(typeof(Closure)), IntPtr.Zero);
                _safeHandle = new Closure.ClosureSafeHandle(handle);

                Closure.Native.@ref(Handle);
                Closure.Native.sink(Handle);
                Closure.Native.set_marshal(Handle, _marshalCallback);
            }

            #endregion

            #region Methods
            
            private void MarshalCallback(IntPtr closure, ref Value returnValue, uint nParamValues,
                Value[] paramValues, IntPtr invocationHint, IntPtr marshalData)
            {
                Debug.Assert(
                    condition: paramValues.Length == nParamValues,
                    message: "Values were not marshalled correctly. Breakage may occur"
                );

                _callback?.Invoke(ref paramValues);
            }

            public void Dispose()
            {
                _safeHandle.Dispose();
            }
            
            #endregion
        }
    }
}
