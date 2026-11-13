using System;

namespace Gst.Internal;

public partial class LogContextBuilderHandle
{
    public partial LogContextBuilderOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial LogContextBuilderUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class LogContextBuilderOwnedHandle
{
    public static partial LogContextBuilderOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotImplementedException();
    }
}
