using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GLabel : GMisc
    {
        public Property<string> Text { get; }

        public GLabel(string text) : this(Gtk.Label.@new(text)) { }
        internal GLabel(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");
        }
    }
}