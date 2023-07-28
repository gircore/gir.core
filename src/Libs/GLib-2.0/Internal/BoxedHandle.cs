using System;
using System.Runtime.InteropServices;

namespace GLib.Internal;

public class BoxedHandle<T> : SafeHandle where T : NativeGTypeProvider
{
    public sealed override bool IsInvalid => handle == IntPtr.Zero;

    public BoxedHandle(IntPtr handle, bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
    {
        if (!ownsHandle)
            handle = Functions.BoxedCopy(GetGType(), handle);

        SetHandle(handle);
    }

    protected override bool ReleaseHandle()
    {
        Functions.BoxedFree(GetGType(), handle);
        return true;
    }

    private static nuint GetGType()
    {
#if NET7_0_OR_GREATER
        return T.GetGType();
#else
        return NativeGTypeProviderHelper.GetGType<T>();
#endif
    }
}

public class InitiallyUnownedBoxedHandle<T> : BoxedHandle<T> where T : NativeGTypeProvider
{
    public InitiallyUnownedBoxedHandle(IntPtr handle) : base(handle, false)
    {
    }
}

public class OwnedBoxedHandle<T> : BoxedHandle<T> where T : NativeGTypeProvider
{
    public OwnedBoxedHandle(IntPtr handle) : base(handle, true)
    {
    }
}
