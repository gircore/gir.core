using System;
using GObject;

namespace Gtk
{
    public partial class Image : IImage
    {
        #region Properties
        public Property<int> PixelSize { get; }
        public Property<string> Name { get; }
        #endregion

        internal Image(IntPtr handle) : base(handle)
        {
            Name = PropertyOfString("icon-name");
            PixelSize = PropertyOfInt("pixel-size");
        }

        public void Clear() => Sys.Image.clear(Handle);
    }
}