using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GBox : GContainer, Box
    {
        internal GBox(IntPtr handle) : base(handle) {}
        public GBox(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) { }

        public void PackStart(GWidget widget, bool expand, bool fill, uint padding) => Gtk.Box.pack_start(this, widget, expand, fill, padding);
    }
}