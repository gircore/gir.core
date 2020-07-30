using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GBox : GWidget
    {
        public GBox() : this(Gtk.Box.@new(Gtk.Orientation.horizontal, 0)) {}
        internal GBox(IntPtr handle) : base(handle) { }

        public void Append(GWidget widget) => Gtk.Box.append(this, widget);
    }
}