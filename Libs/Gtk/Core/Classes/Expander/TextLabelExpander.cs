using System;
using GObject.Core;

namespace Gtk.Core
{
    public class TextLabelExpander : GExpander
    {
        public Property<string> Label { get; }
        public Property<bool> UseMarkup { get; }
        public Property<bool> UseUnderline { get; }

        public TextLabelExpander(string label) : this(Gtk.Expander.@new(label)) { }
        internal TextLabelExpander(IntPtr handle) : base(handle)
        {
            Label = PropertyOfString("label");
            UseMarkup = PropertyOfBool("use-markup");
            UseUnderline = PropertyOfBool("use-underline");
        }
    }
}