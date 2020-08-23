using System;
using GObject;

namespace Gtk
{
    public partial class Expander
    {
        #region  Proerties
        public Property<bool> Expanded { get; }
        public Property<bool> ResizeToplevel { get; }
        public Property<bool> LabelFill { get; }
        public Property<string> Label { get; }
        public Property<bool> UseMarkup { get; }
        public Property<bool> UseUnderline { get; }
        public Property<Widget> Widget { get; }
        #endregion

        internal Expander(IntPtr handle) : base(handle)
        {
            Expanded = PropertyOfBool("expanded");
            ResizeToplevel = PropertyOfBool("resize-toplevel");
            LabelFill = PropertyOfBool("label-fill");
            Label = PropertyOfString("label");
            UseMarkup = PropertyOfBool("use-markup");
            UseUnderline = PropertyOfBool("use-underline");
            
            Widget = Property("label-widget",
                get: GetObject<Widget>,
                set: Set
            );
        }
    }
}