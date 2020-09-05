using System;
using System.Runtime.InteropServices;

namespace GLib
{
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Error
    {
        #region Fields

        private readonly int domain;
        private readonly int code;
        private readonly IntPtr message;

        #endregion

        #region Properties
        public string Message => Marshal.PtrToStringAuto(message);
        #endregion
        
        #region Methods
        internal static void FreeError(IntPtr errorHandle) => free(errorHandle);
        #endregion
    }
}