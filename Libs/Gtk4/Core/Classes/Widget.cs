using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        public void Show() => Sys.Widget.show(Handle);
    }
}