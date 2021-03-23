using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial class Object
    {
        private class ObjectSafeHandle : Native.ClassSafeHandle
        {
            public ObjectSafeHandle(IntPtr handle) : base(handle)
            {
            }

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

                Native.Instance.Methods.Unref(handle);
                return true;
            }
        }
    }
}
