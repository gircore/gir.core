using System;
using GLib;

namespace GdkPixbuf
{
    public partial class PixbufLoader
    {
        public static Pixbuf FromBytes(byte[] data)
        {
            IntPtr handle = Native.PixbufLoader.Instance.Methods.New();

            try
            {
                using var bytes = Bytes.From(data);

                Native.PixbufLoader.Instance.Methods.WriteBytes(handle, bytes.Handle, out var error);
                Error.ThrowOnError(error);

                Native.PixbufLoader.Instance.Methods.Close(handle, out error);
                Error.ThrowOnError(error);

                return new Pixbuf(Native.PixbufLoader.Instance.Methods.GetPixbuf(handle), false);
            }
            finally
            {
                GObject.Internal.Object.Instance.Methods.Unref(handle);
            }
        }
    }
}
