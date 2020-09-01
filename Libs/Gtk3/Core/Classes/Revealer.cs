using System;
using GObject;

namespace Gtk
{
    public partial class Revealer
    {
        public Revealer() : this(Sys.Revealer.@new()) { }
    }
}
