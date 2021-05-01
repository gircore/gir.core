using System;
using System.Runtime.InteropServices;

namespace GObject
{
    /// <summary>
    /// Call Handler for ClosureMarshal  with a simplified interface as long as delegates are not working out.
    /// TOOD: Remove this class
    /// </summary>
    public class ClosureMarshalCallHandlerWorkaround : IDisposable
    {
        public Native.ClosureMarshalCallback NativeCallback;

        private readonly System.Action _managedCallback;

        public ClosureMarshalCallHandlerWorkaround(System.Action managed)
        {
            NativeCallback = NativeCallbackMarshaller;
            _managedCallback = managed;
        }

        private void NativeCallbackMarshaller(IntPtr closure, IntPtr returnValue, uint nParamValues, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] GObject.Native.Value.Struct[] paramValues, IntPtr invocationHint, IntPtr marshalData)
        {
            _managedCallback();
        }

        public void Dispose()
        {
            // This implements IDisposable just to signal to the caller that this class contains
            // disposable state. Actually there is no state which needs to be freed. But if an instance
            // of this object is freed to early the NativeCallback can not be invoked from C anymore
            // which breaks any native code relying on the availability of the NativeCallback.
        }
    }
}
