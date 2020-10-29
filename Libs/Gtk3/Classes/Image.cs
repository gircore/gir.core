using GObject;

namespace Gtk
{
    public partial class Image
    {
        public static Image NewFromIconName(string iconName, IconSize size)
            => new Image(
                ConstructParameter.With(IconNameProperty, iconName),
                ConstructParameter.With(IconSizeProperty, (int)size)
            );

        public static readonly Property<string> IconNameProperty = Property<string>.Register<Image>(
            Native.IconNameProperty,
            nameof(IconName),
            (o) => o.IconName,
            (o, v) => o.IconName = v
        );

        public string IconName
        {
            get => GetProperty(IconNameProperty);
            set => SetProperty(IconNameProperty, value);
        }


        public static readonly Property<int> IconSizeProperty = Property<int>.Register<Image>(
            Native.IconSizeProperty,
            nameof(IconSize),
            (o) => o.IconSize,
            (o, v) => o.IconSize = v
        );

        public int IconSize
        {
            get => GetProperty(IconSizeProperty);
            set => SetProperty(IconSizeProperty, value);
        }
    }
}
