using System;
using System.Runtime.InteropServices;

namespace GLib;

public sealed class GException : Exception, IDisposable
{
    private readonly Internal.ErrorHandle _errorHandle;

    public GException(Internal.ErrorHandle errorHandle)
        : base(Marshal.PtrToStringUTF8(errorHandle.GetMessage()))
    {
        _errorHandle = errorHandle;
    }

    public void Dispose()
    {
        _errorHandle.Dispose();
    }
}
