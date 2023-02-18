using System;
using System.Runtime.InteropServices;
using System.Text;

namespace GLib.Internal;

public abstract class NullableUtf8StringHandle : SafeHandle
{
    protected NullableUtf8StringHandle(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero;

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

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NullableUtf8StringUnownedHandle)}");
    }
}

public class NullableUtf8StringOwnedHandle : NullableUtf8StringHandle
{
    private NullableUtf8StringOwnedHandle() : base(true)
    {
    }

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

    public string ConvertToString()
    {
        if (IsInvalid)
            throw new Exception($"{nameof(NonNullableUtf8StringHandle)} should not have a null handle");

        return StringHelper.ToStringUtf8(handle)!;
    }
}

public class NonNullableUtf8StringUnownedHandle : NonNullableUtf8StringHandle
{
    private NonNullableUtf8StringUnownedHandle() : base(false)
    {
    }

    protected override bool ReleaseHandle()
    {
        throw new InvalidOperationException($"It is not allowed to free a {nameof(NonNullableUtf8StringUnownedHandle)}");
    }
}

public class NonNullableUtf8StringOwnedHandle : NonNullableUtf8StringHandle
{
    private NonNullableUtf8StringOwnedHandle() : base(true)
    {
    }

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
