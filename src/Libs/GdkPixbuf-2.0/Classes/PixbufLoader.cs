using System;
using GLib;

namespace GdkPixbuf
{
    public partial class PixbufLoader
    {
        public static Pixbuf FromBytes(byte[] data)
        {
            IntPtr handle = Internal.PixbufLoader.Instance.Methods.New();

            try
            {
                using var bytes = Bytes.From(data);

                Internal.PixbufLoader.Instance.Methods.WriteBytes(handle, bytes.Handle, out var error);
                Error.ThrowOnError(error);

                Internal.PixbufLoader.Instance.Methods.Close(handle, out error);
                Error.ThrowOnError(error);

                return new Pixbuf(Internal.PixbufLoader.Instance.Methods.GetPixbuf(handle), false);
            }
            finally
            {
                GObject.Internal.Object.Instance.Methods.Unref(handle);
            }
        }
    }
}
