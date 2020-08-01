using System;

namespace Gtk
{
    public abstract class Image : Misc, IImage
    {
        internal Image(IntPtr handle) : base(handle) { }

        public void Clear() => Sys.Image.clear(this);
    }
}