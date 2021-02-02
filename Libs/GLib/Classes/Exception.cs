using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public sealed partial class GException : Exception, IDisposable
    {
        #region Fields

        private readonly GExceptionSafeHandle _errorHandle;

        #endregion

        #region Constructors

        internal GException(IntPtr errorHandle) : base(Marshal.PtrToStructure<Error>(errorHandle).Message)
        {
            _errorHandle = new GExceptionSafeHandle(errorHandle);
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            _errorHandle.Dispose();
        }

        #endregion
    }
}
