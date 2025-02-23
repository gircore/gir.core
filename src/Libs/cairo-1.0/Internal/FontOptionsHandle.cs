namespace Cairo.Internal;

public partial class FontOptionsOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        FontOptions.Destroy(handle);
        return true;
    }
}
