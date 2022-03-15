namespace cairo.Internal
{
    public partial class RegionOwnedHandle : RegionHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Region.Destroy(this);
            return true;
        }
    }
}

