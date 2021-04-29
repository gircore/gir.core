using GObject.Native;

namespace Gtk
{
    public partial class Image
    {
        public static Image New()
            => ObjectWrapper.WrapHandle<Image>(Native.Image.Instance.Methods.New(), false);
        
        public static Image NewFromIconName(string iconName, IconSize size)
            => ObjectWrapper.WrapHandle<Image>(Native.Image.Instance.Methods.NewFromIconName(iconName, size), false);
    }
}
