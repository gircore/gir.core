using System;

namespace Cairo;

public class ImageSurface : Surface
{
    private static readonly GLib.ConstantString DataKey = new("data");

    public ImageSurface(Format format, int width, int height)
        : base(Internal.ImageSurface.Create(format, width, height))
    {
        Handle.AddMemoryPressure(GetSizeInBytes());
    }

    public ImageSurface(GLib.Bytes data, Format format, int width, int height, int stride)
        : base(Internal.ImageSurface.CreateForData(data.Handle.DangerousGetHandle(), format, width, height, stride))
    {
        Handle.AddMemoryPressure(GetSizeInBytes());
        SetUserData(data);
    }

    private void SetUserData(GLib.Bytes data)
    {
        var userDataHandler = new Internal.UserDataHandler(data.Handle);
        Internal.Surface.SetUserData(Handle, DataKey.GetHandle(), data.Handle.DangerousGetHandle(), userDataHandler.DestroyNotify);
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

