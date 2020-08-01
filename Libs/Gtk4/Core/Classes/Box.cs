using System;

namespace Gtk
{
    public class Box : Widget
    {
        public Box() : this(Sys.Box.@new(Sys.Orientation.horizontal, 0)) {}
        internal Box(IntPtr handle) : base(handle) { }

        public void Append(Widget widget) => Sys.Box.append(this, widget);
    }
}