using System;
using System.Runtime.InteropServices;

namespace GObject
{
    public partial struct Closure
    {
        internal class ClosureSafeHandle : SafeHandle
        {
            public ClosureSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
            {
                SetHandle(handle);
            }

            public sealed override bool IsInvalid => handle == IntPtr.Zero;

            protected sealed override bool ReleaseHandle()
            {
                Native.invalidate(handle);
                Native.unref(handle);
                return true;
            }
        }
    }
}
