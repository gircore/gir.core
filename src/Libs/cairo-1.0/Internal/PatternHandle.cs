namespace Cairo.Internal;

public partial class PatternOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        Pattern.Destroy(handle);
        return true;
    }
}
