using System;
using System.Runtime.InteropServices;

namespace GLib;

public struct SListElement
{
    public IntPtr Data;

    /// <summary>
    /// Create managed string from UTF-8 data
    /// </summary>
    /// <returns>String from UTF-8 data of pointer</returns>
    public readonly string? ToStringUTF8()
    {
        return Marshal.PtrToStringUTF8(Data);
    }
}
