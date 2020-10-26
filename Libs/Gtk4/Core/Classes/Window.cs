using System;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        public Window() : this(Sys.Window.@new()) { }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));
        public void SetChild(Widget child) => Sys.Window.set_child(Handle, GetHandle(child));
    }
}
