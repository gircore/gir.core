namespace Cairo.Internal;

public partial class PathOwnedHandle
{
    protected override partial bool ReleaseHandle()
    {
        Path.Destroy(handle);
        return true;
    }
}
