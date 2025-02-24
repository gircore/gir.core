namespace Cairo.Internal;

public partial class ContextOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        Context.Destroy(handle);
        return true;
    }
}
