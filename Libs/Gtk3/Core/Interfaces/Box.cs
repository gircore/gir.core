namespace Gtk
{
    public interface IBox : IContainer
    {
        void PackStart(Widget widget, bool expand, bool fill, uint padding);
    }
}