using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        private class ObjectSafeHandle : SafeHandle
        {
            public ObjectSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
            {
                SetHandle(handle);
            }

            public sealed override bool IsInvalid => handle == IntPtr.Zero;

            protected sealed override bool ReleaseHandle()
            {
                lock (WrapperObjects)
                {
                    WrapperObjects.Remove(handle);
                }

                lock (SubclassObjects)
                {
                    SubclassObjects.Remove(handle);
                }

                Native.unref(handle);
                return true;
            }
        }
    }
}
