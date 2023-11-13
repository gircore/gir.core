using System;

namespace Gdk.Internal;

public partial class DragSurfaceSizeHandle
{
    public partial DragSurfaceSizeOwnedHandle OwnedCopy()
    {
        throw new NotImplementedException();
    }

    public partial DragSurfaceSizeUnownedHandle UnownedCopy()
    {
        throw new NotImplementedException();
    }
}

public partial class DragSurfaceSizeOwnedHandle
{
    public static partial DragSurfaceSizeOwnedHandle FromUnowned(IntPtr ptr)
    {
        throw new NotImplementedException();
    }

    protected override partial bool ReleaseHandle()
    {
        throw new NotImplementedException();
    }
}
