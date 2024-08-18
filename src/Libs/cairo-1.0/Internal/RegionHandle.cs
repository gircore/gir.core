namespace Cairo.Internal;

public partial class RegionOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        Region.Destroy(handle);
        return true;
    }
}
