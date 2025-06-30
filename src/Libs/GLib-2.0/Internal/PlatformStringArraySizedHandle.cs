using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GObject.Internal;

namespace GLib.Internal;

public abstract class PlatformStringArraySizedHandle(bool ownsHandle) : SafeHandle(IntPtr.Zero, ownsHandle)
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_to_utf8")]
    private static extern IntPtr FilenameToUtf8(IntPtr opsysstring, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_from_utf8")]
    private static extern IntPtr FilenameFromUtf8(IntPtr utf8String, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    private int _size = -1;
    public int Size
    {
        get => _size == -1 ? throw new NullReferenceException("Size is not set") : _size;
        set => _size = value;
    }

    protected static IntPtr ToIntPtr(string[] array)
    {
        var data = Functions.Malloc((uint) (array.Length * IntPtr.Size));

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

        List<string> strArray = [];

        for (var offset = 0; offset < Size; offset++)
        {
            var strAddress = Marshal.ReadIntPtr(handle, offset * IntPtr.Size);
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
public class PlatformStringArraySizedOwnedHandle : PlatformStringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArraySizedOwnedHandle() : base(true)
    {
    }

    public PlatformStringArraySizedOwnedHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    public static PlatformStringArraySizedOwnedHandle Create(string[] array)
    {
        return new PlatformStringArraySizedOwnedHandle(ToIntPtr(array))
        {
            Size = array.Length
        };
    }

    protected override bool ReleaseHandle()
    {
        for (var offset = 0; offset < Size; offset++)
        {
            var ptr = Marshal.ReadIntPtr(handle, offset * IntPtr.Size);

            Functions.Free(ptr);
        }

        Functions.Free(handle);
        return true;
    }
}

/// <summary>
/// A handle of a string array. The dotnet runtime only owns the list of pointers to the string. Not the strings itself.
/// </summary>
public class PlatformStringArraySizedContainerHandle : PlatformStringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArraySizedContainerHandle() : base(true)
    {
    }

    public PlatformStringArraySizedContainerHandle(IntPtr ptr) : base(true)
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
public class PlatformStringArraySizedUnownedHandle : PlatformStringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    private static PlatformStringArraySizedUnownedHandle? _nullHandle;
    public static PlatformStringArraySizedUnownedHandle NullHandle => _nullHandle ??= new PlatformStringArraySizedUnownedHandle(IntPtr.Zero);

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private PlatformStringArraySizedUnownedHandle() : base(false)
    {
    }

    public PlatformStringArraySizedUnownedHandle(IntPtr ptr) : base(false)
    {
        SetHandle(ptr);
    }

    public static PlatformStringArraySizedUnownedHandle Create(string[] array)
    {
        return new PlatformStringArraySizedUnownedHandle(ToIntPtr(array))
        {
            Size = array.Length
        };
    }

    protected override bool ReleaseHandle()
    {
        throw new Exception("It is not allowed to free unowned string array handle");
    }
}
