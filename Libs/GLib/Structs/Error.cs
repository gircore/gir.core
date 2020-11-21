using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial struct Error
    {
        #region Properties

        public string Message => Marshal.PtrToStringAnsi(message) ?? System.String.Empty;

        #endregion

        #region Methods

        internal static void FreeError(IntPtr errorHandle) => Native.free(errorHandle);

        #endregion
    }
}
