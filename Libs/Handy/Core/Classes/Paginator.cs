using GObject;
using Gtk;
using System;

namespace Handy
{
    public partial class Paginator
    {
        public Paginator() : this(Sys.Paginator.@new()) { }

        public void Prepend(Widget widget) => Sys.Paginator.prepend(Handle, widget.Handle);
        public void Append(Widget widget) => Sys.Paginator.insert(Handle, widget.Handle, -1);
        public void ScrollTo(Widget widget) => Sys.Paginator.scroll_to(Handle, widget.Handle);
    }
}
