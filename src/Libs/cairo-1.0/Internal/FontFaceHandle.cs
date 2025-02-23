namespace Cairo.Internal;

public partial class FontFaceOwnedHandle
{
    protected override bool ReleaseHandle()
    {
        FontFace.Destroy(handle);
        return true;
    }
}
