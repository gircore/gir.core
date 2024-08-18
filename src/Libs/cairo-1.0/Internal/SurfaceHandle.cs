namespace Cairo.Internal;

public partial class SurfaceOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        Surface.Destroy(handle);
        return true;
    }
}
