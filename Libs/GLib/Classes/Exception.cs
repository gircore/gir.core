using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException : Exception
    {
        #region Properties
        protected Error Error { get; private set; }

        private string? message;
        public override string Message =>  message ??= Marshal.PtrToStringAuto(Error.Message);
        #endregion Properties

        public GException(ref Error error)
        {
            Error = error;
        }

        private void Free() => Error.Free();
    }
}