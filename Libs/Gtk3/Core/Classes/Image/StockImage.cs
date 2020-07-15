using System;
using GObject.Core;

namespace Gtk.Core
{
    public class StockImage : GImage
    {
        public Property<int> PixelSize { get; }
        public Property<string> Name { get; } 

        public StockImage(string name, IconSize size) : this(Gtk.Image.new_from_icon_name(name, (Gtk.IconSize)size)) {}
        internal StockImage(IntPtr handle) : base(handle)
        {
            Name = PropertyOfString("icon-name");
            PixelSize = PropertyOfInt("pixel-size");
        }
    }
}