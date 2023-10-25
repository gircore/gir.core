using System;
using System.Runtime.InteropServices;

namespace GObject.Internal;

public partial class ParamSpecPoolHandle
{
    public partial ParamSpecPoolOwnedHandle OwnedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    public partial ParamSpecPoolUnownedHandle UnownedCopy()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}

public partial class ParamSpecPoolOwnedHandle
{
    public static partial ParamSpecPoolOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotSupportedException("Can't create a copy of this handle");
    }
}
