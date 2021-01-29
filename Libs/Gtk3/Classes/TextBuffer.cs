using GObject;

namespace Gtk
{
    public partial class TextBuffer
    {
        // TODO: Support GtkTextTagTable constructor
        public TextBuffer() { }

        public static readonly Property<string> TextProperty = Property<string>.Register<TextBuffer>(
            Native.TextProperty,
            nameof(Text),
            (o) => o.Text,
            (o, v) => o.Text = v
        );

        public string Text
        {
            get => GetProperty(TextProperty);
            set => SetProperty(TextProperty, value);
        }
    }
}
