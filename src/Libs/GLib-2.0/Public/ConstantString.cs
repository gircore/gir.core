using System;

namespace GLib;

/// <summary>
/// This represents an UTF8 string in unmanaged memory. As long as the instance
/// is kept alive the unmanaged memory will not be freed and the handle will
/// be valid.
///
/// This is useful in cases where C expects some pointer to a string which is not
/// allowed to be freed. This class can be used to keep a pointer to a string
/// alive as long as needed.
/// </summary>
public class ConstantString : IDisposable
{
    private readonly Internal.NonNullableUtf8StringOwnedHandle _handle;

    /// <summary>
    /// Creates a new instance which contains a pointer to a copy of the given string.
    /// </summary>
    /// <param name="str">A string which should be represented in unmanaged memory.</param>
    public ConstantString(string str)
    {
        _handle = Internal.NonNullableUtf8StringOwnedHandle.Create(str);
    }

    /// <summary>
    /// Creates a new string from the string which is stored in unmanaged memory.
    /// </summary>
    /// <returns>The resulting string.</returns>
    public string GetString()
    {
        return _handle.ConvertToString();
    }

    /// <summary>
    /// Retrieves a pointer to the unmanaged memory containing a copy of the original string.
    /// </summary>
    /// <returns>A pointer to unmanaged memory.</returns>
    public IntPtr GetHandle()
    {
        return _handle.DangerousGetHandle();
    }

    /// <summary>
    /// Releases all unmanaged resources
    /// </summary>
    public void Dispose()
    {
        _handle.Dispose();
    }
}
