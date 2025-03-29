using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using GObject.Internal;

namespace GLib.Internal;

public abstract class Utf8StringArraySizedHandle(bool ownsHandle) : SafeHandle(IntPtr.Zero, ownsHandle)
{
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
            Marshal.WriteIntPtr(data, i * IntPtr.Size, StringHelper.StringToPtrUtf8(array[i]));

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
            var ptr = Marshal.ReadIntPtr(handle, offset * IntPtr.Size);
            strArray.Add(Marshal.PtrToStringUTF8(ptr) ?? string.Empty);
        }

        return strArray.ToArray();
    }
}

/// <summary>
/// A handle of a string array which is completely owned by the dotnet runtime
/// </summary>
public class Utf8StringArraySizedOwnedHandle : Utf8StringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArraySizedOwnedHandle() : base(true)
    {
    }

    public Utf8StringArraySizedOwnedHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    public static Utf8StringArraySizedOwnedHandle Create(string[] array)
    {
        return new Utf8StringArraySizedOwnedHandle(ToIntPtr(array))
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
public class Utf8StringArraySizedContainerHandle : PlatformStringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArraySizedContainerHandle() : base(true)
    {
    }

    public Utf8StringArraySizedContainerHandle(IntPtr ptr) : base(true)
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
public class Utf8StringArraySizedUnownedHandle : PlatformStringArraySizedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    private static Utf8StringArraySizedUnownedHandle? _nullHandle;
    public static Utf8StringArraySizedUnownedHandle NullHandle => _nullHandle ??= new Utf8StringArraySizedUnownedHandle(IntPtr.Zero);

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArraySizedUnownedHandle() : base(false)
    {
    }

    public Utf8StringArraySizedUnownedHandle(IntPtr ptr) : base(false)
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
