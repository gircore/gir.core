using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial struct Error
    {
        #region Properties

        public string Message => Marshal.PtrToStringAnsi(message) ?? string.Empty;
        public uint Domain => domain;
        public int Code => code;

        #endregion

        #region Methods

        internal static void FreeError(IntPtr errorHandle) => Native.free(errorHandle);

        #endregion

        public static void ThrowOnError(IntPtr error)
        {
            if (error != IntPtr.Zero)
                throw new GLib.GException(error);
        }
    }
}
