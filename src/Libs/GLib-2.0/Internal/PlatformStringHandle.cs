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

    public string ConvertToString()
    {
        if (IsInvalid)
            throw new Exception($"{nameof(NonNullablePlatformStringHandle)} should not have a null handle");

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

public class NonNullablePlatformStringOwnedHandle : NonNullablePlatformStringHandle
{
    private NonNullablePlatformStringOwnedHandle() : base(true)
    {
    }

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
