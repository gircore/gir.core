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

                var error = new GLib.Native.Error.Handle(IntPtr.Zero);
                Native.PixbufLoader.Instance.Methods.WriteBytes(handle, bytes.Handle, error);
                Error.ThrowOnError(error);

                Native.PixbufLoader.Instance.Methods.Close(handle, error);
                Error.ThrowOnError(error);

                return new Pixbuf(Native.PixbufLoader.Instance.Methods.GetPixbuf(handle), false);
            }
            finally
            {
                GObject.Native.Object.Instance.Methods.Unref(handle);
            }
        }
    }
}
