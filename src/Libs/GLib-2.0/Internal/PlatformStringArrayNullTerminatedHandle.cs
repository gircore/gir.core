using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GObject.Internal;

namespace GLib.Internal;

public abstract class PlatformStringArrayNullTerminatedHandle : SafeHandle
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_to_utf8")]
    internal static extern IntPtr FilenameToUtf8(IntPtr opsysstring, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_from_utf8")]
    internal static extern IntPtr FilenameFromUtf8(IntPtr utf8string, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    protected PlatformStringArrayNullTerminatedHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    protected static IntPtr ToIntPtr(string[] array)
    {
        var data = Functions.Malloc((uint) ((array.Length + 1) * IntPtr.Size));

        for (var i = 0; i < array.Length; i++)
        {
            var utf8Ptr = IntPtr.Zero;

            try
            {
                utf8Ptr = StringHelper.StringToPtrUtf8(array[i]);
                var filenamePtr = FilenameFromUtf8(utf8Ptr, -1, out _, out _, out var error);

                if (!error.IsInvalid)
                    throw new GException(error);

                Marshal.WriteIntPtr(data, i * IntPtr.Size, filenamePtr);
            }
            finally
            {
                Functions.Free(utf8Ptr);
            }
        }

        //Null terminate array
        Marshal.WriteIntPtr(data, array.Length * IntPtr.Size, IntPtr.Zero);

        return data;
    }

    /// <summary>
    /// Converts the data of this handle into a managed string array.
    /// </summary>
    /// <returns>A string array containing the handle data .</returns>
    public string[]? ConvertToStringArray()
    {
        if (handle == IntPtr.Zero)
            return null;

        List<string> strArray = new();
        var offset = 0;

        while (true)
        {
            var strAddress = Marshal.ReadIntPtr(handle, offset++ * IntPtr.Size);

            if (strAddress == IntPtr.Zero)
                break;

            var utf8Ptr = FilenameToUtf8(strAddress, -1, out _, out _, out var error);

            if (!error.IsInvalid)
                throw new GException(error);

            strArray.Add(Marshal.PtrToStringUTF8(utf8Ptr) ?? string.Empty);
        }

        return strArray.ToArray();
    }
}

/// <summary>
/// A handle of a string array which is completely owned by the dotnet runtime
/// </summary>
public class PlatformStringArrayNullTerminatedOwnedHandle : PlatformStringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArrayNullTerminatedOwnedHandle() : base(true)
    {
    }

    public PlatformStringArrayNullTerminatedOwnedHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    public static PlatformStringArrayNullTerminatedOwnedHandle Create(string[] array)
    {
        return new PlatformStringArrayNullTerminatedOwnedHandle(ToIntPtr(array));
    }

    protected override bool ReleaseHandle()
    {
        var offset = 0;
        while (true)
        {
            var strAddress = Marshal.ReadIntPtr(handle, offset++ * IntPtr.Size);

            if (strAddress == IntPtr.Zero)
                break;

            Functions.Free(strAddress);
        }

        Functions.Free(handle);
        return true;
    }
}

/// <summary>
/// A handle of a string array. The dotnet runtime only owns the list of pointers to the string. Not the strings itself.
/// </summary>
public class PlatformStringArrayNullTerminatedContainerHandle : PlatformStringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArrayNullTerminatedContainerHandle() : base(true)
    {
    }

    public PlatformStringArrayNullTerminatedContainerHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    protected override bool ReleaseHandle()
    {
        //Only free the list, not the strings itself
        Functions.Free(handle);
        return true;
    }
}

/// <summary>
/// A handle of a string array which is not owned by the dotnet runtime.
/// </summary>
public class PlatformStringArrayNullTerminatedUnownedHandle : PlatformStringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    private static PlatformStringArrayNullTerminatedUnownedHandle? _nullHandle;
    public static PlatformStringArrayNullTerminatedUnownedHandle NullHandle => _nullHandle ??= new PlatformStringArrayNullTerminatedUnownedHandle(IntPtr.Zero);

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArrayNullTerminatedUnownedHandle() : base(false)
    {
    }

    public PlatformStringArrayNullTerminatedUnownedHandle(IntPtr ptr) : base(false)
    {
        SetHandle(ptr);
    }

    public static PlatformStringArrayNullTerminatedUnownedHandle Create(string[] array)
    {
        return new PlatformStringArrayNullTerminatedUnownedHandle(ToIntPtr(array));
    }

    protected override bool ReleaseHandle()
    {
        throw new Exception("It is not allowed to free unowned string array handle");
    }
}
