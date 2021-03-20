using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public sealed partial class GException : Exception, IDisposable
    {
        #region Fields

        private readonly Error.Native.ErrorSafeHandle _errorHandle;

        #endregion

        #region Constructors

        internal GException(Error.Native.ErrorSafeHandle errorHandle) 
            : base(Marshal.PtrToStructure<Error.Native.Struct>(errorHandle.DangerousGetHandle()).Message)
        {
            _errorHandle = errorHandle;
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
