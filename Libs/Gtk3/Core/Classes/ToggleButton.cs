using System;
using GObject;

namespace Gtk
{
    public partial class ToggleButton
    {
        public ToggleButton(string text) : this(Sys.ToggleButton.new_with_label(text)) { }
    }
}
