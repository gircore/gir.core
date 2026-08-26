using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GLib;

/// <summary>
/// A GException wraps a GLib.Error and allows to catch it with a given error code.
/// </summary>
[DebuggerDisplay("GException: ErrorDomain={ErrorCode.GetErrorDomain()}, ErrorCode={ErrorCode.Code}")]
public sealed class GException(Internal.ErrorHandle errorHandle)
    : Exception(Marshal.PtrToStringUTF8(errorHandle.GetMessage())), IDisposable
{
    /// <summary>
    /// The <see cref="GLib.ErrorCode"/> of this exception.
    /// </summary>
    public ErrorCode ErrorCode => new()
    {
        Domain = errorHandle.GetDomain(),
        Code = errorHandle.GetCode()
    };

    /// <summary>
    /// Compare if this exception matches the given <see cref="GLib.ErrorCode"/>.
    /// </summary>
    /// <param name="errorCode">The error code to check.</param>
    /// <returns>TRUE if the given error code matches the exception, otherwise FALSE.</returns>
    public bool Matches(ErrorCode errorCode)
    {
        return Internal.Error.Matches(errorHandle, errorCode.Domain, errorCode.Code);
    }

    /// <summary>
    /// Compare if this exception matches the given domain regardless of any error code.
    /// </summary>
    /// <param name="domain">The domain to check.</param>
    /// <returns>TRUE if the given domain matches the domain of the exception, otherwise FALSE.</returns>
    public bool Matches(Quark domain)
    {
        return errorHandle.GetDomain() == domain;
    }

    public void Dispose()
    {
        errorHandle.Dispose();
    }
}
