using System;
using System.Runtime.InteropServices;

namespace GLib;

public sealed partial class GException : Exception, IDisposable
{
    #region Fields

    private readonly Internal.ErrorHandle _errorHandle;

    #endregion

    #region Constructors

    internal GException(Internal.ErrorHandle errorHandle)
        : base(Marshal.PtrToStructure<Internal.ErrorData>(errorHandle.DangerousGetHandle()).Message)
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
