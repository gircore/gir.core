using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract class NullableUtf8StringHandle : SafeHandle
{
    protected NullableUtf8StringHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Converts the data of this handle into a managed string. In case of a NULL
    /// handle this returns null.
    /// </summary>
    /// <returns>A string containing the handle data or null.</returns>
    public string? ConvertToString()
    {
        return StringHelper.ToStringUtf8(handle);
    }
}

public class NullableUtf8StringUnownedHandle : NullableUtf8StringHandle
{
    private NullableUtf8StringUnownedHandle() : base(false)
    {
    }

    /// <summary>
    /// Creates a nullable utf8 string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A nullable utf8 string handle</returns>
    public static NullableUtf8StringUnownedHandle Create(string? s)
    {
        var alloc = new NullableUtf8StringUnownedHandle();
        alloc.SetHandle(StringHelper.StringToPtrUtf8(s));
        return alloc;
    }

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NullableUtf8StringUnownedHandle)}");
    }
}

/// <summary>
/// Represents an owned nullable utf8 string. If an instance of this class
/// is collected by the garbage collecctor the associated memory is freed.
/// </summary>
public class NullableUtf8StringOwnedHandle : NullableUtf8StringHandle
{
    private NullableUtf8StringOwnedHandle() : base(true)
    {
    }

    /// <summary>
    /// Creates a nullable utf8 string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A nullable utf8 string handle</returns>
    public static NullableUtf8StringOwnedHandle Create(string? s)
    {
        var alloc = new NullableUtf8StringOwnedHandle();
        alloc.SetHandle(StringHelper.StringToPtrUtf8(s));
        return alloc;
    }

    protected override bool ReleaseHandle()
    {
        GLib.Internal.Functions.Free(handle);
        return true;
    }
}

public abstract class NonNullableUtf8StringHandle : SafeHandle
{
    protected NonNullableUtf8StringHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Converts the data of this handle into a managed string. In case of a NULL
    /// handle this throws a <see cref="GLib.Internal.NullHandleException"/>.
    /// </summary>
    /// <returns>A string containing the handle data.</returns>
    /// <exception cref="GLib.Internal.NullHandleException">Thrown in case of a NULL handle</exception>
    public string ConvertToString()
    {
        if (IsInvalid)
            throw new NullHandleException($"{nameof(NonNullableUtf8StringHandle)} should not have a null handle");

        return StringHelper.ToStringUtf8(handle)!;
    }
}

public class NonNullableUtf8StringUnownedHandle : NonNullableUtf8StringHandle
{
    private NonNullableUtf8StringUnownedHandle() : base(false)
    {
    }

    /// <summary>
    /// Creates a non nullable utf8 string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A non nullable utf8 string handle</returns>
    public static NonNullableUtf8StringUnownedHandle Create(string s)
    {
        var alloc = new NonNullableUtf8StringUnownedHandle();
        alloc.SetHandle(StringHelper.StringToPtrUtf8(s));
        return alloc;
    }

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NonNullableUtf8StringUnownedHandle)}");
    }
}

/// <summary>
/// Represents an owned non nullable utf8 string. If an instance of this class
/// is collected by the garbage collecctor the associated memory is freed.
/// </summary>
public class NonNullableUtf8StringOwnedHandle : NonNullableUtf8StringHandle
{
    private NonNullableUtf8StringOwnedHandle() : base(true)
    {
    }

    /// <summary>
    /// Creates a non nullable utf8 string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A non nullable utf8 string handle</returns>
    public static NonNullableUtf8StringOwnedHandle Create(string s)
    {
        var alloc = new NonNullableUtf8StringOwnedHandle();
        alloc.SetHandle(StringHelper.StringToPtrUtf8(s));
        return alloc;
    }

    protected override bool ReleaseHandle()
    {
        GLib.Internal.Functions.Free(handle);
        return true;
    }
}
