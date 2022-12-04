using System;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public class ObjectHandle : SafeHandle
{
    public IntPtr Handle => IsInvalid ? IntPtr.Zero : DangerousGetHandle();

    public ObjectHandle(IntPtr handle, object obj, bool ownedRef) : base(IntPtr.Zero, true)
    {
        ObjectMapper.Map(handle, obj, ownedRef);
        SetHandle(handle);
    }

    public sealed override bool IsInvalid => handle == IntPtr.Zero;

    protected sealed override bool ReleaseHandle()
    {
        ObjectMapper.Unmap(handle);
        return true;
    }
}
