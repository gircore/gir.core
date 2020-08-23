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

        public void Clear() => Sys.Image.clear(Handle);
    }
}