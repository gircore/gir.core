namespace Cairo;

public class SurfacePattern : Pattern
{
    public SurfacePattern(Surface surface)
        : base(Internal.Pattern.CreateForSurface(surface.Handle))
    {
    }

    public Surface GetSurface()
    {
        Internal.Pattern.GetSurface(Handle, out var surface_handle);
        return new Surface(surface_handle.OwnedCopy());
    }
}
