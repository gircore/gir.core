using GObject;

namespace Gtk
{
    public partial class Label
    {
        #region Properties

        public static readonly Property<string> TextProperty = Property<string>.Register<Label>(
            Native.LabelProperty,
            nameof(Label),
            (o) => o.Text,
            (o, v) => o.Text = v
        );

        public string Text
        {
            get => GetProperty(TextProperty);
            set => SetProperty(TextProperty, value);
        }

        #endregion

        public Label(string text) : this(ConstructParameter.With(TextProperty, text)){ }
    }
}