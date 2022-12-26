using System;

namespace Cairo;

public class ImageSurface : Surface
{
    public ImageSurface(Format format, int width, int height)
        : base(Internal.ImageSurface.Create(format, width, height))
    {
    }

    public Format Format => Internal.ImageSurface.GetFormat(Handle);
    public int Height => Internal.ImageSurface.GetHeight(Handle);
    public int Width => Internal.ImageSurface.GetWidth(Handle);
    public int Stride => Internal.ImageSurface.GetStride(Handle);

    public Span<byte> GetData()
    {
        IntPtr data = Internal.ImageSurface.GetData(Handle);
        int len = Stride * Height; // Stride is the number of bytes per row.
        unsafe
        {
            return new Span<byte>(data.ToPointer(), len);
        }
    }
}

