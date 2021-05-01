using GObject.Native;

namespace Gtk
{
    public partial class Box
    {
        public static Box New(Gtk.Orientation orientation, int spacing)
            => new(Native.Box.Instance.Methods.New(orientation, spacing), false);
    }
}
