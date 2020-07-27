using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public partial class GWidget : GObject.Core.GObject
    {
        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal GWidget(IntPtr handle) : base(handle, true) 
        {
            WidthRequest = PropertyOfInt("width-request");
            HeightRequest = PropertyOfInt("height-request");
        }

        public void Show() => Gtk.Widget.show(this);
    }
}