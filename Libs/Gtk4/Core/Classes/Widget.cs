using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public void Show() => Sys.Widget.show(Handle);
    }
}
