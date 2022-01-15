namespace Gtk
{
    public partial class Image
    {
        public static Image New()
            => new(Internal.Image.New(), false);

        public static Image NewFromIconName(string iconName, IconSize size)
            => new(Internal.Image.NewFromIconName(iconName, size), false);
    }
}
