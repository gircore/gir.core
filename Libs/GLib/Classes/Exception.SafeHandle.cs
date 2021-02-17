using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException
    {
        private class GExceptionSafeHandle : SafeHandle
        {
            public GExceptionSafeHandle(IntPtr handle) : base(IntPtr.Zero, true)
            {
                SetHandle(handle);
            }

            public sealed override bool IsInvalid => handle == IntPtr.Zero;

            protected sealed override bool ReleaseHandle()
            {
                Error.FreeError(handle);
                return true;
            }
        }
    }
}
