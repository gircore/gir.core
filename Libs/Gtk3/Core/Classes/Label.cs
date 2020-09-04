using System;
using GObject;

namespace Gtk
{
    public partial class Label
    {
        public Label(string text) : this(Sys.Label.@new(text)) { }
    }
}
