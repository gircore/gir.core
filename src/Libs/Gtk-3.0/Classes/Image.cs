using GObject.Native;

namespace Gtk
{
    public partial class Image
    {
        public static Image New()
            => new(Native.Image.Instance.Methods.New(), false);

        public static Image NewFromIconName(string iconName, IconSize size)
            => new(Native.Image.Instance.Methods.NewFromIconName(iconName, size), false);
    }
}
