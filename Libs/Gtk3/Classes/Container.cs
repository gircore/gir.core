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

        public void Add(Widget widget) => Native.add(Handle, widget.Handle);
        public void Remove(Widget widget) => Native.remove(Handle, widget.Handle);

        public void SetBorderWidth(uint border_width) => Native.set_border_width(Handle, border_width);
    }
}
