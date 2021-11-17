using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public sealed partial class GException : Exception, IDisposable
    {
        #region Fields

        private readonly Internal.Error.Handle _errorHandle;

        #endregion

        #region Constructors

        internal GException(Internal.Error.Handle errorHandle)
            : base(Marshal.PtrToStructure<Internal.Error.Struct>(errorHandle.DangerousGetHandle()).Message)
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
