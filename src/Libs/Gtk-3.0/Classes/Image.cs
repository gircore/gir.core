using GObject.Internal;

namespace Gtk
{
    public partial class Image
    {
        public static Image New()
            => new(Internal.Image.Instance.Methods.New(), false);

        public static Image NewFromIconName(string iconName, IconSize size)
            => new(Internal.Image.Instance.Methods.NewFromIconName(iconName, size), false);
    }
}
