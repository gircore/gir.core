namespace Cairo;

public partial class Surface
{
    // Cached to avoid creating new references for each property access.
    private Device? _device;

    public Content Content => Internal.Surface.GetContent(Handle);
    public Device Device => _device ??= new Device(Internal.Surface.GetDevice(Handle));
    public Status Status => Internal.Surface.Status(Handle);
    public SurfaceType SurfaceType => Internal.Surface.GetType(Handle);

    public (double X, double Y) DeviceOffset
    {
        get
        {
            Internal.Surface.GetDeviceOffset(Handle, out double xOffset, out double yOffset);
            return (xOffset, yOffset);
        }
        set => Internal.Surface.SetDeviceOffset(Handle, value.X, value.Y);
    }

    public (double X, double Y) DeviceScale
    {
        get
        {
            Internal.Surface.GetDeviceScale(Handle, out double xScale, out double yScale);
            return (xScale, yScale);
        }
        set => Internal.Surface.SetDeviceScale(Handle, value.X, value.Y);
    }

    public (double X, double Y) FallbackResolution
    {
        get
        {
            Internal.Surface.GetFallbackResolution(Handle, out double xPixelsPerInch, out double yPixelsPerInch);
            return (xPixelsPerInch, yPixelsPerInch);
        }
        set => Internal.Surface.SetFallbackResolution(Handle, value.X, value.Y);
    }

    public void GetFontOptions(FontOptions options)
        => Internal.Surface.GetFontOptions(Handle, options.Handle);

    public Surface CreateSimilar(Content content, int width, int height)
        => new Surface(Internal.Surface.CreateSimilar(Handle, content, width, height));

    public Surface CreateSimilarImage(Format format, int width, int height)
        => new Surface(Internal.Surface.CreateSimilarImage(Handle, format, width, height));

    public Surface CreateForRectangle(double x, double y, double width, double height)
        => new Surface(Internal.Surface.CreateForRectangle(Handle, x, y, width, height));

    public void Finish() => Internal.Surface.Finish(Handle);

    public void Flush() => Internal.Surface.Flush(Handle);

    public void MarkDirty() => Internal.Surface.MarkDirty(Handle);

    public void MarkDirty(int x, int y, int width, int height)
        => Internal.Surface.MarkDirtyRectangle(Handle, x, y, width, height);
}
