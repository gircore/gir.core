using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GHeaderBar : GContainer
    {
        public Property<string> Title { get; }
        public Property<string> Subtitle { get; }
        public Property<bool> ShowCloseButton { get; }

        public GHeaderBar() : this(Gtk.HeaderBar.@new()){}
        internal GHeaderBar(IntPtr handle) : base(handle) 
        {
            Title = Property<string>("title",
                get: GetStr, 
                set: Set
            );

            Subtitle = Property<string>("subtitle",
                get: GetStr, 
                set: Set
            );

            ShowCloseButton = Property<bool>("show-close-button",
                get: GetBool,
                set: Set
            );
        }
    }
}