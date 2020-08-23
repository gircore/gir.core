using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public IProperty<int> WidthRequest { get; }
        public IProperty<int> HeightRequest { get; }

        public void Show() => Sys.Widget.show(Handle);
    }
}