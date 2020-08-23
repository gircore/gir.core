using System;
using System.Reflection;

namespace Gtk
{
    public partial class Box : IBox
    {
        public Box(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) { }

        public void PackStart(Widget widget, bool expand, bool fill, uint padding) => Sys.Box.pack_start(Handle, GetHandle(widget), expand, fill, padding);
    }
}