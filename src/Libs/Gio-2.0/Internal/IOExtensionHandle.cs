using System;

namespace Gio.Internal;

public abstract partial class IOExtensionHandle
{
    public partial IOExtensionOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial IOExtensionUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class IOExtensionOwnedHandle
{
    public static partial IOExtensionOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}
