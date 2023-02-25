using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public abstract class NullablePlatformStringHandle : SafeHandle
{
    protected NullablePlatformStringHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    // Manual binding since the generated binding requires a non-nullable handle.
    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_to_utf8")]
    private static extern NonNullableUtf8StringOwnedHandle FilenameToUtf8(IntPtr opsysstring, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    /// <summary>
    /// Converts the data of this handle into a managed string. In case of a NULL
    /// pointer this returns null.
    /// </summary>
    /// <returns>A string containing the handle data or null.</returns>
    /// <exception cref="GLib.GException">Thrown if the string could not be converted into utf8.</exception>
    public string? ConvertToString()
    {
        if (IsInvalid)
            return null;

        NonNullableUtf8StringOwnedHandle result = FilenameToUtf8(handle, -1, out _, out _, out var error);
        GLib.Error.ThrowOnError(error);
        return result.ConvertToString();
    }
}

public class NullablePlatformStringUnownedHandle : NullablePlatformStringHandle
{
    private NullablePlatformStringUnownedHandle() : base(false)
    {
    }

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NullablePlatformStringUnownedHandle)}");
    }
}

public class NullablePlatformStringOwnedHandle : NullablePlatformStringHandle
{
    private NullablePlatformStringOwnedHandle() : base(true)
    {
    }

    // Manual binding since the generated binding returns a non-nullable handle.
    [DllImport(ImportResolver.Library, EntryPoint = "g_filename_from_utf8")]
    private static extern IntPtr FilenameFromUtf8(NonNullableUtf8StringHandle utf8string, long len, out nuint bytesRead, out nuint bytesWritten, out ErrorOwnedHandle error);

    /// <summary>
    /// Creates a nullable platform string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A nullable platform string handle</returns>
    /// <exception cref="GLib.GException">Thrown if the string could not be converted into a platform string.</exception>
    public static NullablePlatformStringOwnedHandle Create(string? s)
    {
        var handle = new NullablePlatformStringOwnedHandle();
        if (s is not null)
        {
            handle.SetHandle(FilenameFromUtf8(NonNullableUtf8StringOwnedHandle.Create(s), -1, out _, out _, out var error));
            GLib.Error.ThrowOnError(error);
        }

        return handle;
    }

    protected override bool ReleaseHandle()
    {
        GLib.Internal.Functions.Free(handle);
        return true;
    }
}

public abstract class NonNullablePlatformStringHandle : SafeHandle
{
    protected NonNullablePlatformStringHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

    /// <summary>
    /// Converts the data of this handle into a managed string. In case of a NULL
    /// handle this throws a <see cref="GLib.Internal.NullHandleException"/>.
    /// </summary>
    /// <returns>A string containing the handle data.</returns>
    /// <exception cref="GLib.Internal.NullHandleException">Thrown in case of a NULL handle</exception>
    /// <exception cref="GLib.GException">Thrown if the string could not be converted into a utf8 string.</exception>
    public string ConvertToString()
    {
        if (IsInvalid)
            throw new NullHandleException($"{nameof(NonNullablePlatformStringHandle)} should not have a null handle");

        NonNullableUtf8StringOwnedHandle result = GLib.Internal.Functions.FilenameToUtf8(this, -1, out _, out _, out var error);
        GLib.Error.ThrowOnError(error);
        return result.ConvertToString();
    }
}

public class NonNullablePlatformStringUnownedHandle : NonNullablePlatformStringHandle
{
    private NonNullablePlatformStringUnownedHandle() : base(false)
    {
    }

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NonNullablePlatformStringUnownedHandle)}");
    }
}

/// <summary>
/// Represents an owned non nullable platform string. If an instance of this class
/// is collected by the garbage collecctor the associated memory is freed.
/// </summary>
public class NonNullablePlatformStringOwnedHandle : NonNullablePlatformStringHandle
{
    private NonNullablePlatformStringOwnedHandle() : base(true)
    {
    }

    /// <summary>
    /// Creates a non nullable platform string handle for the given string.
    /// </summary>
    /// <param name="s">The string which should be used to create the handle.</param>
    /// <returns>A non nullable utf8 string handle</returns>
    /// <exception cref="GLib.GException">Thrown if the string could not be converted into a platform string.</exception>
    public static NonNullablePlatformStringOwnedHandle Create(string s)
    {
        var handle = GLib.Internal.Functions.FilenameFromUtf8(NonNullableUtf8StringOwnedHandle.Create(s), -1, out _, out _, out var error);
        GLib.Error.ThrowOnError(error);
        return handle;
    }

    protected override bool ReleaseHandle()
    {
        GLib.Internal.Functions.Free(handle);
        return true;
    }
}
