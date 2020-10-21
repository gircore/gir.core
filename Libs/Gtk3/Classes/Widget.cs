using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public void ShowAll() => Native.show_all(Handle);
    }
}