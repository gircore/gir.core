namespace Cairo.Internal;

public partial class PatternOwnedHandle : PatternHandle
{
    protected override partial bool ReleaseHandle()
    {
        Pattern.Destroy(handle);
        return true;
    }
}
