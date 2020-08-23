using System;

namespace Gtk
{
    public partial class CheckButton
    {
        public CheckButton(string text) : this(Sys.CheckButton.new_with_label(text)) {}

        internal CheckButton(IntPtr handle) : base(handle) { }
    }
}