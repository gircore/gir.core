using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Widget : GObject.InitiallyUnowned
    {
        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal protected Widget(params ConstructProp[] properties) : base(properties) {}

        internal new static GObject.Sys.Type GetGType() => new GObject.Sys.Type(Sys.Widget.get_type());

        internal Widget(IntPtr handle) : base(handle)
        {
            WidthRequest = PropertyOfInt("width-request");
            HeightRequest = PropertyOfInt("height-request");
        }

        public void Show() => Sys.Widget.show(this.Handle);
        public void ShowAll() => Sys.Widget.show_all(this.Handle);
    }
}