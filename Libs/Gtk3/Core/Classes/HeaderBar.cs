using System;
using GObject;

namespace Gtk
{
    public partial class HeaderBar
    {
        #region Properties
        public Property<string> Title { get; }
        public Property<string> Subtitle { get; }
        public Property<bool> ShowCloseButton { get; }
        #endregion

        public HeaderBar() : this(Sys.HeaderBar.@new()){}
    }
}