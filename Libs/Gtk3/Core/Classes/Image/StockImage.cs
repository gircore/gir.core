using System;
using GObject;

namespace Gtk
{
    public class StockImage : Image
    {
        public Property<int> PixelSize { get; }
        public Property<string> Name { get; } 

        public StockImage(string name, IconSize size) : this(Sys.Image.new_from_icon_name(name, (Sys.IconSize)size)) {}
        internal StockImage(IntPtr handle) : base(handle)
        {
            Name = PropertyOfString("icon-name");
            PixelSize = PropertyOfInt("pixel-size");
        }
    }
}