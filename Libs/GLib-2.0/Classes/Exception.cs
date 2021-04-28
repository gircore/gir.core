using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public sealed partial class GException : Exception, IDisposable
    {
        #region Fields

        private readonly Native.Error.Handle _errorHandle;

        #endregion

        #region Constructors

        internal GException(Native.Error.Handle errorHandle) 
            : base(Marshal.PtrToStructure<Native.Error.Struct>(errorHandle.DangerousGetHandle()).Message)
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
