namespace Gtk.Core
{
    public interface Box : Container
    {
        void PackStart(GWidget widget, bool expand, bool fill, uint padding);
    }
}