using System;
using System.Reflection;

namespace Gtk
{
    public class Container : Widget, IContainer
    {
        internal Container(string template, string obj, Assembly assembly) : base(template, obj, assembly){}
        internal protected Container(IntPtr handle) : base(handle) {}

        public void Add(Widget widget) => Sys.Container.add(this, widget);
    }
}