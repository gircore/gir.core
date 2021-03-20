using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial record Error
    {
        #region Methods

        internal static void FreeError(Native.ErrorSafeHandle errorHandle) => Native.Methods.Free(errorHandle);

        #endregion

        public static void ThrowOnError(Native.ErrorSafeHandle error)
        {
            if (!error.IsInvalid)
                throw new GException(error);
        }
    }
}
