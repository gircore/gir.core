using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class Bytes
    {
        private class BytesSafeHandle : SafeHandle
        {
            public BytesSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
            {
                SetHandle(handle);
            }

            public sealed override bool IsInvalid => handle == IntPtr.Zero;

            protected sealed override bool ReleaseHandle()
            {
                Native.Methods.Unref(handle);
                return true;
            }
        }
    }
}
