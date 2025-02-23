using System;

namespace Cairo;

public class ImageSurface : Surface
{
    public ImageSurface(Format format, int width, int height)
        : base(Internal.ImageSurface.Create(format, width, height))
    {
        Handle.AddMemoryPressure(GetSizeInBytes());
    }

    public Format Format => Internal.ImageSurface.GetFormat(Handle);
    public int Height => Internal.ImageSurface.GetHeight(Handle);
    public int Width => Internal.ImageSurface.GetWidth(Handle);

    /// <summary>
    /// Number of bytes per row.
    /// </summary>
    public int Stride => Internal.ImageSurface.GetStride(Handle);

    private int GetSizeInBytes() => Stride * Height;

    public Span<byte> GetData()
    {
        var data = Internal.ImageSurface.GetData(Handle);

        unsafe
        {
            return new Span<byte>(data.ToPointer(), GetSizeInBytes());
        }
    }
}

