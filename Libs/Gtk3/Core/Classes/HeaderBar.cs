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
        internal HeaderBar(IntPtr handle) : base(handle) 
        {
            Title = PropertyOfString("title");
            Subtitle = PropertyOfString("subtitle");
            ShowCloseButton = PropertyOfBool("show-close-button");
        }
    }
}