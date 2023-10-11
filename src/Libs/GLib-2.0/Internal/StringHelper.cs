using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GLib.Internal;

public static class StringHelper
{

    /// <summary>
    /// Creates a null-terminated UTF-8 string in unmanaged memory.
    /// </summary>
    /// <returns>A pointer to a null-terminated UTF-8 string.</returns>
    /// <remarks>The result should later be freed with g_free().</remarks>
    public static IntPtr StringToPtrUtf8(string? str)
    {
        if (str is null)
            return IntPtr.Zero;

        var bytes = Encoding.UTF8.GetBytes(str);
        var alloc = Functions.Malloc((uint) (bytes.Length + 1));
        Marshal.Copy(bytes, 0, alloc, bytes.Length);
        Marshal.WriteByte(alloc, bytes.Length, 0);

        return alloc;
    }
}

