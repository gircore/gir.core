namespace cairo.Internal
{
    public partial class SurfaceOwnedHandle : SurfaceHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Surface.Destroy(this);
            return true;
        }
    }
}

