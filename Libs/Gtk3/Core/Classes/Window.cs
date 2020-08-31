using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Window
    {
       
        public Window() : this(Sys.Window.@new(Sys.WindowType.toplevel)) { }
        public Window(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly())
        {
        }
        internal Window(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));
    }
}