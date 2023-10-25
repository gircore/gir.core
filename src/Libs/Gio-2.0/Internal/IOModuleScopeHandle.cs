using System;

namespace Gio.Internal;

public abstract partial class IOModuleScopeHandle
{
    public partial IOModuleScopeOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial IOModuleScopeUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class IOModuleScopeOwnedHandle
{
    public static partial IOModuleScopeOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

