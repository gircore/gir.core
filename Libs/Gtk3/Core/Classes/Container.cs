using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GContainer : GWidget, Container
    {
        internal GContainer(string template, string obj, Assembly assembly) : base(template, obj, assembly){}
        internal protected GContainer(IntPtr handle) : base(handle) {}

        public void Add(GWidget widget) => Gtk.Container.add(this, widget);
    }
}