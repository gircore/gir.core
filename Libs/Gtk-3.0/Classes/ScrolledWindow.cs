using GObject;

namespace Gtk
{
    public partial class ScrolledWindow
    {
        public ScrolledWindow() { }

        public void SetMinContentWidth(int width) => Native.set_min_content_width(Handle, width);
        public void SetMinContentHeight(int height) => Native.set_min_content_height(Handle, height);

        public void SetMaxContentWidth(int width) => Native.set_max_content_width(Handle, width);
        public void SetMaxContentHeight(int height) => Native.set_max_content_height(Handle, height);
    }
}
