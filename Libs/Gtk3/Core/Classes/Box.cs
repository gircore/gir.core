using System;
using System.Reflection;

namespace Gtk
{
    public class Box : Container, IBox
    {
        internal Box(IntPtr handle) : base(handle) {}
        public Box(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) { }

        public void PackStart(Widget widget, bool expand, bool fill, uint padding) => Sys.Box.pack_start(this, widget, expand, fill, padding);
    }
}