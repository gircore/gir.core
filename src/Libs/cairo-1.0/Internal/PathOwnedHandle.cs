namespace Cairo.Internal;

public partial class PathOwnedHandle : PathHandle
{
    protected override partial bool ReleaseHandle()
    {
        Path.Destroy(handle);
        return true;
    }
}
