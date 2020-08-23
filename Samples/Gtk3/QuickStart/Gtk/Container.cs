using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public class Container : Widget
    {
        internal protected Container(IntPtr handle) : base(handle) {}
        internal protected Container(params ConstructProp[] properties) : base(properties) {}

        internal new static GObject.Sys.Type GetGType() => new GObject.Sys.Type(Sys.Container.get_type());

        public void Add(Widget widget) => Sys.Container.add(this.Handle, widget.Handle);
    }
}