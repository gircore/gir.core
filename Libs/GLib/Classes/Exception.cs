using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException : Exception
    {
        private readonly IntPtr errorHandle;

        #region Properties
        protected Error? error;
        protected Error Error => error ??= Marshal.PtrToStructure<GLib.Error>(errorHandle);

        private string? message;
        public override string Message =>  message ??= Marshal.PtrToStringAuto(Error.Message);
        #endregion Properties

        public GException(IntPtr error)
        {
            this.errorHandle = error;
        }

        private void Free() => Error.free(errorHandle);
    }
}