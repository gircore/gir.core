using System;

namespace Gtk
{
    public partial class Box
    {
        public Box() : this(Sys.Box.@new(Sys.Orientation.horizontal, 0)) {}

        public void Append(Widget widget) => Sys.Box.append(Handle,  widget.Handle);
    }
}
