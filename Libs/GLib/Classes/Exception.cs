using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException : Exception
    {
        #region Properties
        private Error Error { get; }
        
        private string? message;
        public override string Message => message ??= Marshal.PtrToStringAuto(Error.Message);
        #endregion Properties

        public GException(IntPtr errorHandle)
        {
            Error = Marshal.PtrToStructure<GLib.Error>(errorHandle);
        }

        private void Free() => Error.Free();
    }
}