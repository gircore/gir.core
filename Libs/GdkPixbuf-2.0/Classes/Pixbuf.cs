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

        #region Properties

        #region WidthProperty

        public static readonly Property<int> WidthProperty = Property<int>.Wrap<Pixbuf>(
            Properties.Width,
            nameof(Width),
            get: (o) => o.Width,
            set: (o, v) => o.Width = v
        );

        public int Width
        {
            get => GetProperty(WidthProperty);
            set => SetProperty(WidthProperty, value);
        }

        #endregion

        #region HeightProperty

        public static readonly Property<int> HeightProperty = Property<int>.Wrap<Pixbuf>(
            Properties.Height,
            nameof(Height),
            get: (o) => o.Height,
            set: (o, v) => o.Height = v
        );

        public int Height
        {
            get => GetProperty(HeightProperty);
            set => SetProperty(HeightProperty, value);
        }

        #endregion

        #endregion

        public static Pixbuf NewFromFile(string fileName)
        {
            IntPtr handle = Native.Pixbuf.Instance.Methods.NewFromFile(fileName, out var error);
            Error.ThrowOnError(error);

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
