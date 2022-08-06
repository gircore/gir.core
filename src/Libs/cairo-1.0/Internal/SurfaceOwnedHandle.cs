namespace Cairo.Internal
{
    public partial class SurfaceOwnedHandle : SurfaceHandle
    {
        protected override partial bool ReleaseHandle()
        {
            Surface.Destroy(handle);
            return true;
        }
    }
}

