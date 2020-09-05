using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException : Exception
    {
        #region Fields
        private readonly IntPtr errorHandle;
        #endregion

        public GException(IntPtr errorHandle) : base(Marshal.PtrToStructure<Error>(errorHandle).Message)
        {
            this.errorHandle = errorHandle;
        }

        private void Free() => Error.FreeError(errorHandle);
    }
}