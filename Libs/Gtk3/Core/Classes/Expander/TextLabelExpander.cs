using System;
using GObject;

namespace Gtk
{
    public class TextLabelExpander : Expander
    {
        public Property<string> Label { get; }
        public Property<bool> UseMarkup { get; }
        public Property<bool> UseUnderline { get; }

        public TextLabelExpander(string label) : this(Sys.Expander.@new(label)) { }
        internal TextLabelExpander(IntPtr handle) : base(handle)
        {
            Label = PropertyOfString("label");
            UseMarkup = PropertyOfBool("use-markup");
            UseUnderline = PropertyOfBool("use-underline");
        }
    }
}