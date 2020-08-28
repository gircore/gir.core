using System;
using GObject;

namespace Gtk
{
    public partial class HeaderBar
    {
        #region Properties
        public IProperty<string> Title { get; }
        public IProperty<string> Subtitle { get; }
        public IProperty<bool> ShowCloseButton { get; }
        #endregion

        public HeaderBar() : this(Sys.HeaderBar.@new()) { }
    }
}