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
            using var bytes = Bytes.New(data);

            Internal.PixbufLoader.WriteBytes(handle, bytes.Handle, out var error);

            if (!error.IsInvalid)
                throw new GException(error);

            Internal.PixbufLoader.Close(handle, out error);

            if (!error.IsInvalid)
                throw new GException(error);

            return new Pixbuf(Internal.PixbufLoader.GetPixbuf(handle), false);
        }
        finally
        {
            GObject.Internal.Object.Unref(handle);
        }
    }
}
