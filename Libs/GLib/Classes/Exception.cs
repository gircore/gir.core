using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public partial class GException : Exception
    {
        #region Fields

        private readonly IntPtr _errorHandle;

        #endregion

        #region Constructors

        public GException(IntPtr errorHandle) : base(Marshal.PtrToStructure<Error>(errorHandle).Message)
        {
            _errorHandle = errorHandle;
        }

        public GException()
        { }

        public GException(string message) : base(message)
        { }

        public GException(string message, Exception innerException) : base(message, innerException)
        { }

        #endregion

        #region Methods

        private void Free() => Error.FreeError(_errorHandle);

        #endregion
    }
}
