using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract class Utf8StringArrayNullTerminatedHandle : SafeHandle
{
    protected Utf8StringArrayNullTerminatedHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    protected static IntPtr ToIntPtr(string[] array)
    {
        var data = Functions.Malloc((uint) ((array.Length + 1) * IntPtr.Size));

        for (var i = 0; i < array.Length; i++)
            Marshal.WriteIntPtr(data, i * IntPtr.Size, StringHelper.StringToPtrUtf8(array[i]));

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

            strArray.Add(Marshal.PtrToStringUTF8(strAddress) ?? string.Empty);
        }

        return strArray.ToArray();
    }
}

/// <summary>
/// A handle of a string array which is completely owned by the dotnet runtime
/// </summary>
public class Utf8StringArrayNullTerminatedOwnedHandle : Utf8StringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArrayNullTerminatedOwnedHandle() : base(true)
    {
    }

    public Utf8StringArrayNullTerminatedOwnedHandle(IntPtr ptr) : base(true)
    {
        SetHandle(ptr);
    }

    public static Utf8StringArrayNullTerminatedOwnedHandle Create(string[] array)
    {
        return new Utf8StringArrayNullTerminatedOwnedHandle(ToIntPtr(array));
    }

    [DllImport(ImportResolver.Library, EntryPoint = "g_strfreev")]
    private static extern void StrFreeV(IntPtr strArray);

    protected override bool ReleaseHandle()
    {
        StrFreeV(handle);
        return true;
    }
}

/// <summary>
/// A handle of a string array. The dotnet runtime only owns the list of pointers to the string. Not the strings itself.
/// </summary>
public class Utf8StringArrayNullTerminatedContainerHandle : Utf8StringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArrayNullTerminatedContainerHandle() : base(true)
    {
    }

    public Utf8StringArrayNullTerminatedContainerHandle(IntPtr ptr) : base(true)
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
public class Utf8StringArrayNullTerminatedUnownedHandle : Utf8StringArrayNullTerminatedHandle
{
    public override bool IsInvalid => handle == IntPtr.Zero;

    private static Utf8StringArrayNullTerminatedUnownedHandle? _nullHandle;
    public static Utf8StringArrayNullTerminatedUnownedHandle NullHandle => _nullHandle ??= new Utf8StringArrayNullTerminatedUnownedHandle(IntPtr.Zero);

    /// <summary>
    /// Used by PInvoke
    /// </summary>
    private Utf8StringArrayNullTerminatedUnownedHandle() : base(false)
    {
    }

    public Utf8StringArrayNullTerminatedUnownedHandle(IntPtr ptr) : base(false)
    {
        SetHandle(ptr);
    }

    public static Utf8StringArrayNullTerminatedUnownedHandle Create(string[] array)
    {
        return new Utf8StringArrayNullTerminatedUnownedHandle(ToIntPtr(array));
    }

    protected override bool ReleaseHandle()
    {
        throw new Exception("It is not allowed to free unowned string array handle");
    }
}
