using GObject;

namespace Gtk
{
    public partial class Expander
    {
        public static readonly Property<string> LabelProperty = Property<string>.Register<Expander>(
            Native.LabelProperty,
            nameof(Label),
            (o) => o.Label,
            (o, v) => o.Label = v
        );

        public string Label
        {
            get => GetProperty(LabelProperty);
            set => SetProperty(LabelProperty, value);
        }

        public static readonly Property<Widget> LabelWidgetProperty = Property<Widget>.Register<Expander>(
            Native.LabelWidgetProperty,
            nameof(LabelWidget),
            (o) => o.LabelWidget,
            (o, v) => o.LabelWidget = v
        );

        public Widget LabelWidget
        {
            get => GetProperty(LabelWidgetProperty);
            set => SetProperty(LabelWidgetProperty, value);
        }


        public static readonly Property<bool> ResizeToplevelProperty = Property<bool>.Register<Expander>(
            Native.ResizeToplevelProperty,
            nameof(ResizeToplevel),
            (o) => o.ResizeToplevel,
            (o, v) => o.ResizeToplevel = v
        );

        public bool ResizeToplevel
        {
            get => GetProperty(ResizeToplevelProperty);
            set => SetProperty(ResizeToplevelProperty, value);
        }

        public Expander(string label) : this(ConstructParameter.With(LabelProperty, label)) { }
    }
}
