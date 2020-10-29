using GObject;

namespace Gtk
{
    public partial class Label
    {
        #region Properties

        public static readonly Property<string> TextProperty = Property<string>.Register<Label>(
            Native.LabelProperty,
            nameof(Text),
            (o) => o.Text,
            (o, v) => o.Text = v
        );

        public string Text
        {
            get => GetProperty(TextProperty);
            set => SetProperty(TextProperty, value);
        }


        public static readonly Property<bool> UseMarkupProperty = Property<bool>.Register<Label>(
            Native.UseMarkupProperty,
            nameof(UseMarkup),
            (o) => o.UseMarkup,
            (o, v) => o.UseMarkup = v
        );

        public bool UseMarkup
        {
            get => GetProperty(UseMarkupProperty);
            set => SetProperty(UseMarkupProperty, value);
        }


        public static readonly Property<Justification> JustifyProperty = Property<Justification>.Register<Label>(
            Native.JustifyProperty,
            nameof(Justify),
            (o) => o.Justify,
            (o, v) => o.Justify = v
        );

        public Justification Justify
        {
            get => GetProperty(JustifyProperty);
            set => SetProperty(JustifyProperty, value);
        }


        public static readonly Property<bool> WrapProperty = Property<bool>.Register<Label>(
            Native.WrapProperty,
            nameof(Wrap),
            (o) => o.Wrap,
            (o, v) => o.Wrap = v
        );

        public bool Wrap
        {
            get => GetProperty(WrapProperty);
            set => SetProperty(WrapProperty, value);
        }


        public static readonly Property<bool> SelectableProperty = Property<bool>.Register<Label>(
            Native.SelectableProperty,
            nameof(Selectable),
            (o) => o.Selectable,
            (o, v) => o.Selectable = v
        );

        public bool Selectable
        {
            get => GetProperty(SelectableProperty);
            set => SetProperty(SelectableProperty, value);
        }


        public static readonly Property<bool> UseUnderlineProperty = Property<bool>.Register<Label>(
            Native.UseUnderlineProperty,
            nameof(UseUnderline),
            (o) => o.UseUnderline,
            (o, v) => o.UseUnderline = v
        );

        public bool UseUnderline
        {
            get => GetProperty(UseUnderlineProperty);
            set => SetProperty(UseUnderlineProperty, value);
        }


        public static readonly Property<float> XAlignProperty = Property<float>.Register<Label>(
            Native.XalignProperty,
            nameof(XAlign),
            (o) => o.XAlign,
            (o, v) => o.XAlign = v
        );

        public float XAlign
        {
            get => GetProperty(XAlignProperty);
            set => SetProperty(XAlignProperty, value);
        }


        public static readonly Property<float> YAlignProperty = Property<float>.Register<Label>(
            Native.YalignProperty,
            nameof(YAlign),
            (o) => o.YAlign,
            (o, v) => o.YAlign = v
        );

        public float YAlign
        {
            get => GetProperty(YAlignProperty);
            set => SetProperty(YAlignProperty, value);
        }

        #endregion

        public Label(string text) : this(ConstructParameter.With(TextProperty, text)){ }
    }
}
