using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial record Error
    {
        #region Methods

        internal static void FreeError(IntPtr errorHandle) => Native.Methods.Free(errorHandle);

        #endregion

        public static void ThrowOnError(IntPtr error)
        {
            if (error != IntPtr.Zero)
                throw new GException(error);
        }
    }
}
