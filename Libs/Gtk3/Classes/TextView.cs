using GObject;

namespace Gtk
{
    public partial class TextView
    {
        public TextView() {}

        public static readonly Property<TextBuffer> BufferProperty = Property<TextBuffer>.Register<TextView>(
            Native.BufferProperty,
            nameof(Buffer),
            (o) => o.Buffer,
            (o, v) => o.Buffer = v
        );

        public TextBuffer Buffer
        {
            get => GetProperty(BufferProperty);
            set => SetProperty(BufferProperty, value);
        }

        public static readonly Property<bool> EditableProperty = Property<bool>.Register<TextView>(
            Native.EditableProperty,
            nameof(Editable),
            (o) => o.Editable,
            (o, v) => o.Editable = v
        );

        public bool Editable
        {
            get => GetProperty(EditableProperty);
            set => SetProperty(EditableProperty, value);
        }
    }
}
