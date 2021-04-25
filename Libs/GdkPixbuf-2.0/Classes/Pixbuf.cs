using System;
using GLib;
using GObject;

namespace GdkPixbuf
{
    public partial class Pixbuf
    {
        #region Fields

        private long _size;

        #endregion

        public static Pixbuf NewFromFile(string fileName)
        {
            var error = new GLib.Native.Error.Handle(IntPtr.Zero);
            IntPtr handle = Native.Pixbuf.Instance.Methods.NewFromFile(fileName, error);
            Error.ThrowOnError(error);
            
            error.Dispose();

            return new Pixbuf(handle, true);
        }

        protected override void Initialize()
        {
            base.Initialize();
            _size = (long) Native.Pixbuf.Instance.Methods.GetByteLength(Handle);
            GC.AddMemoryPressure(_size);
        }

        public override void Dispose()
        {
            base.Dispose();
            GC.RemoveMemoryPressure(_size);
        }
    }
}
