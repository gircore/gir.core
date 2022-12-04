using System;
using GLib;

namespace GdkPixbuf;

public partial class PixbufLoader
{
    public static Pixbuf FromBytes(byte[] data)
    {
        IntPtr handle = Internal.PixbufLoader.New();

        try
        {
            using var bytes = Bytes.From(data);

            Internal.PixbufLoader.WriteBytes(handle, bytes.Handle, out var error);
            Error.ThrowOnError(error);

            Internal.PixbufLoader.Close(handle, out error);
            Error.ThrowOnError(error);

            return new Pixbuf(Internal.PixbufLoader.GetPixbuf(handle), false);
        }
        finally
        {
            GObject.Internal.Object.Unref(handle);
        }
    }
}
