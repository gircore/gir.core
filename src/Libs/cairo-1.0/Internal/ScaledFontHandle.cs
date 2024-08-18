namespace Cairo.Internal;

public partial class ScaledFontOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        ScaledFont.Destroy(handle);
        return true;
    }
}
