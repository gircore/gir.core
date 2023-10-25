using System;

namespace Gdk.Internal;

public abstract partial class ToplevelSizeHandle
{
    public partial ToplevelSizeOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial ToplevelSizeUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class ToplevelSizeOwnedHandle
{
    public static partial ToplevelSizeOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}
