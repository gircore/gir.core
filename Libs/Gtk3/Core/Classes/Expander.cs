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
    }
}