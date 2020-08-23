using System;
using System.Reflection;

namespace Gtk
{
    public partial class Container : IContainer
    {
        internal Container(string template, string obj, Assembly assembly) : base(template, obj, assembly){}

        public void Add(Widget widget) => Sys.Container.add(Handle, GetHandle(widget));
    }
}