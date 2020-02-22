using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GCheckButton : GToggleButton
    {
        public GCheckButton(string text) : this(Gtk.CheckButton.new_with_label(text)) {}

        internal GCheckButton(IntPtr handle) : base(handle) { }
    }
}