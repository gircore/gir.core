using System;
using GObject;

namespace Gtk
{
    public partial class Label
    {
        public Property<string> Text { get; }

        public Label(string text) : this(Sys.Label.@new(text)) { }
        internal Label(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");
        }
    }
}