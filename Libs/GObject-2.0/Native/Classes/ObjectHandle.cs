using System;
using System.Runtime.InteropServices;

namespace GObject.Native
{
    public class ObjectHandle : SafeHandle
    {
        public ObjectHandle(IntPtr handle) : base(IntPtr.Zero, true)
        {
            SetHandle(handle);
        }

        public sealed override bool IsInvalid => handle == IntPtr.Zero;

        protected sealed override bool ReleaseHandle()
        {
            ObjectMapper.Unmap(handle);
            Object.Instance.Methods.Unref(handle);

            return true;
        }
    }
}
