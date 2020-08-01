using System;
using GObject;

namespace Gtk
{
    public class Widget : GObject.Object
    {
        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal Widget(IntPtr handle) : base(handle, true) 
        {
            WidthRequest = PropertyOfInt("width-request");
            HeightRequest = PropertyOfInt("height-request");
        }

        public void Show() => Sys.Widget.show(this);
    }
}