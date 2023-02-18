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
    public static string? ToStringUtf8(IntPtr ptr)
        => Marshal.PtrToStringUTF8(ptr);

    /// <summary>
    /// Marshals each pointer in the IntPtr array as a UTF-8 encoded string.
    /// </summary>
    /// <param name="ptrArray"></param>
    /// <returns>A managed version of the string array.</returns>
    /// <remarks>This method does not free the unmanaged strings represented by ptr.</remarks>
    public static string[] ToStringArrayUtf8(IntPtr[] ptrArray)
        => ptrArray.Select(x => ToStringUtf8(x) ?? string.Empty).ToArray();

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
            strArray.Add(ToStringUtf8(strAddress) ?? string.Empty);
        }

        return strArray.ToArray();
    }

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
        IntPtr alloc = GLib.Internal.Functions.Malloc((uint) (bytes.Length + 1));
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

        // Populate with UTF-8 encoded bytes
        for (var i = 0; i < numStrings; i++)
        {
            // Get null-terminated UTF-8 byte array
            var bytes = Encoding.UTF8.GetBytes(array[i] + '\0');

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
