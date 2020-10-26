using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial struct Error
    {
        #region Properties

        public string Message => Marshal.PtrToStringAuto(message);

        #endregion

        #region Methods

        internal static void FreeError(IntPtr errorHandle) => free(errorHandle);

        #endregion
    }
}