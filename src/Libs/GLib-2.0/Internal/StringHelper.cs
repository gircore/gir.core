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

    /// <summary>
    /// Creates a managed string from a null-terminated UTF-8 string in unmanaged memory
    /// which is not owned by the runtime.
    /// </summary>
    /// <param name="ptr">A pointer to a null-terminated UTF-8 string.</param>
    /// <returns>A string containing the data of the given pointer.</returns>
    public static string ToStringUtf8(IntPtr ptr)
    {
        return Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
    }

    /// <summary>
    /// Creates a managed string from a null-terminated UTF-8 string in unmanaged memory
    /// which is owned by the runtime and frees it.
    /// </summary>
    /// <param name="ptr">A pointer to a null-terminated UTF-8 string.</param>
    /// <returns>A string containing the data of the given pointer.</returns>
    public static string ToStringUtf8AndFree(IntPtr ptr)
    {
        var result = ToStringUtf8(ptr);
        Functions.Free(ptr);

        return result;
    }
}

