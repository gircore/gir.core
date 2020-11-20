using System;
using GObject;

namespace Gtk
{
    public partial class Box : Orientable
    {
        // TODO: Temporary Property - Define in interface Gtk.Orientable
        public static readonly Property<Orientation> OrientationProperty = Property<Orientation>.Register<Box>(
            "orientation", // FIXME: Don't use hardcoded properties
            nameof(Orientation),
            (o) => o.Orientation,
            (o, v) => o.Orientation = v
        );

        public Orientation Orientation
        {
            get => GetProperty(OrientationProperty);
            set => SetProperty(OrientationProperty, value);
        }


        public static readonly Property<int> SpacingProperty = Property<int>.Register<Box>(
            Native.SpacingProperty,
            nameof(Spacing),
            (o) => o.Spacing,
            (o, v) => o.Spacing = v
        );

        public int Spacing
        {
            get => GetProperty(SpacingProperty);
            set => SetProperty(SpacingProperty, value);
        }

        public void PackStart(Widget widget, bool expand, bool fill, uint padding)
            => Native.pack_start(Handle, GetHandle(widget), expand, fill, padding);

        public Box(Orientation orientation) : this(orientation, 0) {}

        public Box(Orientation orientation, int spacing)
            : this(
                ConstructParameter.With(OrientationProperty, orientation),
                ConstructParameter.With(SpacingProperty, spacing)
            )
        { }
    }
}
