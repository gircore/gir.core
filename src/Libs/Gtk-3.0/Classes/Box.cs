using GObject.Internal;

namespace Gtk
{
    public partial class Box
    {
        public static Box New(Gtk.Orientation orientation, int spacing)
            => new(Internal.Box.Instance.Methods.New(orientation, spacing), false);
    }
}
