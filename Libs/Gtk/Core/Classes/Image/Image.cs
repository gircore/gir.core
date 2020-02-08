using System;

namespace Gtk.Core
{
    public abstract class GImage : GMisc, Image
    {
        internal GImage(IntPtr handle) : base(handle) { }

        public void Clear() => Gtk.Image.clear(this);
    }
}