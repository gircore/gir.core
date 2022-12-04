namespace Cairo.Internal;

public partial class ScaledFontOwnedHandle : ScaledFontHandle
{
    protected override partial bool ReleaseHandle()
    {
        ScaledFont.Destroy(handle);
        return true;
    }
}
