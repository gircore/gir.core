using System;
using GObject;

namespace Gtk
{
    public partial interface Orientable
    {
        // TODO: GInterfaces + Property Support

        /*public static readonly Property<Orientation> OrientationProperty = Property<Orientation>.Register<Orientable>(
            Native.OrientationProperty,
            nameof(Orientation),
            (o) => o.Orientation,
            (o, v) => o.Orientation = v
        );

        public Orientation Orientation
        {
            get => GetProperty(OrientationProperty);
            set => SetProperty(OrientationProperty, value);
        }*/
    }
}
