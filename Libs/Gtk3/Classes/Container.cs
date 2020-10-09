using GObject;

namespace Gtk
{
    public partial class Container
    {
        public static readonly Property<Widget> ChildProperty = Property<Widget>.Register<Container>(
            Native.ChildProperty,
            nameof(Child),
            set: (o, v) => o.Child = v
        );

        public Widget Child
        {
            set => SetProperty(ChildProperty, value);
        }
    }
}