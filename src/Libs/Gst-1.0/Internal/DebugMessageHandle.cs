using System;

namespace Gst.Internal;

public partial class DebugMessageHandle
{
    public partial DebugMessageOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial DebugMessageUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class DebugMessageOwnedHandle
{
    public static partial DebugMessageOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotImplementedException();
    }
}
