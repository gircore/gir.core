using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public sealed partial class GException : Exception, IDisposable
    {
        #region Fields

        private readonly Native.ErrorSafeHandle _errorHandle;

        #endregion

        #region Constructors

        internal GException(Native.ErrorSafeHandle errorHandle) 
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
