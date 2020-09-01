using System;
using GObject;

namespace Gtk
{
    public partial class Image : IImage
    {
        public void Clear() => Sys.Image.clear(Handle);
    }
}
