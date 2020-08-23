using System;
using GObject;

namespace Gtk
{
    public partial class Label
    {
        public IProperty<string> Text { get; }

        public Label(string text) : this(Sys.Label.@new(text)) { }
    }
}