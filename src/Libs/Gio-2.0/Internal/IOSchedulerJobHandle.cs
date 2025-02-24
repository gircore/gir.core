using System;

namespace Gio.Internal;

public abstract partial class IOSchedulerJobHandle
{
    public partial IOSchedulerJobOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial IOSchedulerJobUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class IOSchedulerJobOwnedHandle
{
    public static partial IOSchedulerJobOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}
