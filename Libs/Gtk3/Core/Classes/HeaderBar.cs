using System;
using GObject;

namespace Gtk
{
    public partial class HeaderBar
    {
        public HeaderBar() : this(Sys.HeaderBar.@new()) { }
    }
}
