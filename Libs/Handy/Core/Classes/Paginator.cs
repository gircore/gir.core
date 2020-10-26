using GObject;
using Gtk;
using System;

namespace Handy
{
    public partial class Paginator
    {
        public Paginator() : this(Sys.Paginator.@new()) { }

        public void Prepend(Widget widget) => Sys.Paginator.prepend(Handle, GetHandle(widget));
        public void Append(Widget widget) => Sys.Paginator.insert(Handle, GetHandle(widget), -1);
        public void ScrollTo(Widget widget) => Sys.Paginator.scroll_to(Handle, GetHandle(widget));
    }
}
