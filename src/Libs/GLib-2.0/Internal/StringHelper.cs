using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GLib.Internal;

public static class StringHelper
{
    /// <summary>
    /// Interprets the given ptr as a non nullable string.
    /// </summary>
    /// <returns>a managed version of the string.</returns>
    /// <remarks>This method does not free the unmanaged string represented by ptr.</remarks>
    public static string ToStringUtf8(IntPtr ptr)
        => Marshal.PtrToStringUTF8(ptr) ?? string.Empty;

    /// <summary>
    /// Interprets the given ptr as a nullable string.
    /// </summary>
    /// <returns>a managed version of the string.</returns>
    /// <remarks>This method does not free the unmanaged string represented by ptr.</remarks>
    public static string? ToNullableStringUtf8(IntPtr ptr)
        => (ptr != IntPtr.Zero) ? Marshal.PtrToStringUTF8(ptr) : null;

    /// <summary>
    /// Marshals each pointer in the IntPtr array as a UTF-8 encoded string.
    /// </summary>
    /// <param name="ptrArray"></param>
    /// <returns>A managed version of the string array.</returns>
    /// <remarks>This method does not free the unmanaged strings represented by ptr.</remarks>
    public static string[] ToStringArrayUtf8(IntPtr[] ptrArray)
        => ptrArray.Select(ToStringUtf8).ToArray();

    /// <summary>
    /// Interprets the given ptr as a null terminated string array.
    /// </summary>
    /// <returns>A managed version of the string array.</returns>
    /// <remarks>This method does not free the unmanaged strings represented by ptr.</remarks>
    public static string[] ToStringArrayUtf8(IntPtr ptr)
    {
        if (ptr == IntPtr.Zero)
            return System.Array.Empty<string>();

        // Build string array
        List<string> strArray = new();
        var offset = 0;

        while (true)
        {
            // Read in the next pointer in memory
            IntPtr strAddress = Marshal.ReadIntPtr(ptr, offset++ * IntPtr.Size);

            // Break if we reach the end of the array
            if (strAddress == IntPtr.Zero)
                break;

            // Marshal string and repeat
            strArray.Add(ToStringUtf8(strAddress));
        }

        return strArray.ToArray();
    }

    public static IntPtr StringToHGlobalUTF8(string str)
    {
        // For some methods/delegates (e.g. TranslateFunc), we need to return
        // a string that Glib will own and we cannot free. Create a new
        // null-terminated string in unmanaged memory and pass it to GLib.

        // TODO: Check if GLib needs to free this

        byte[] bytes = Encoding.UTF8.GetBytes(str);
        IntPtr alloc = Marshal.AllocHGlobal(bytes.Length + 1);
        Marshal.Copy(bytes, 0, alloc, bytes.Length);
        Marshal.WriteByte(alloc, bytes.Length, 0);

        return alloc;
    }
}

public class StringArrayNullTerminatedSafeHandle : SafeHandle
{
    private GCHandle _gcHandle;
    private readonly IntPtr[] _data;

    public StringArrayNullTerminatedSafeHandle(string[] array) : base(IntPtr.Zero, true)
    {
        var numStrings = array.Length;
        _data = new IntPtr[numStrings + 1];

        // UTF-8 Encoding Information
        Encoding encoding = System.Text.Encoding.UTF8;

        // Populate with UTF-8 encoded bytes
        for (var i = 0; i < numStrings; i++)
        {
            // Get null-terminated UTF-8 byte array
            var bytes = encoding.GetBytes(array[i] + '\0');

            // Marshal as pointer
            IntPtr ptr = Marshal.AllocHGlobal(bytes.Length);
            Marshal.Copy(bytes, 0, ptr, bytes.Length);

            // Save in data array
            _data[i] = ptr;
        }

        // Null terminate the array
        _data[numStrings] = IntPtr.Zero;

        // Keep in memory until done
        _gcHandle = GCHandle.Alloc(_data, GCHandleType.Pinned);
        SetHandle(_gcHandle.AddrOfPinnedObject());
    }

    protected override bool ReleaseHandle()
    {
        // Free string memory
        foreach (IntPtr ptr in _data)
            Marshal.FreeHGlobal(ptr);

        // Allow GC to free memory
        _gcHandle.Free();

        return true;
    }

    public override bool IsInvalid => !_gcHandle.IsAllocated;
}
