using System;
using GObject;

namespace Gtk
{
    public partial class Button
    {
         #region Properties

        public static readonly Property<bool> AlwaysShowImageProperty = Property<bool>.Register<Button>(
            "always-show-image",
            nameof(AlwaysShowImage),
            (o) => o.AlwaysShowImage,
            (o, v) => o.AlwaysShowImage = v
        );

        public bool AlwaysShowImage
        {
            get => GetProperty(AlwaysShowImageProperty);
            set => SetProperty(AlwaysShowImageProperty, value);
        }

        public static readonly Property<Widget> ImageProperty = Property<Widget>.Register<Button>(
            "image",
            nameof(Image),
            (o) => o.Image,
            (o, v) => o.Image = v
        );

        public Widget Image
        {
            get => GetProperty(ImageProperty);
            set => SetProperty(ImageProperty, value);
        }

        public static readonly Property<PositionType> ImagePositionProperty = Property<PositionType>.Register<Button>(
            "image-position",
            nameof(ImagePosition),
            (o) => o.ImagePosition,
            (o, v) => o.ImagePosition = v
        );

        public PositionType ImagePosition
        {
            get => GetProperty(ImagePositionProperty);
            set => SetProperty(ImagePositionProperty, value);
        }

        public static readonly Property<string> LabelProperty = Property<string>.Register<Button>(
            "label",
            nameof(Label),
            (o) => o.Label,
            (o, v) => o.Label = v
        );

        public string Label
        {
            get => GetProperty(LabelProperty);
            set => SetProperty(LabelProperty, value);
        }

        public static readonly Property<ReliefStyle> ReliefProperty = Property<ReliefStyle>.Register<Button>(
            "relief",
            nameof(Relief),
            (o) => o.Relief,
            (o, v) => o.Relief = v
        );

        public ReliefStyle Relief
        {
            get => GetProperty(ReliefProperty);
            set => SetProperty(ReliefProperty, value);
        }

        public static readonly Property<bool> UseUnderlineProperty = Property<bool>.Register<Button>(
            "use-underline",
            nameof(UseUnderline),
            (o) => o.UseUnderline,
            (o, v) => o.UseUnderline = v
        );

        public bool UseUnderline
        {
            get => GetProperty(UseUnderlineProperty);
            set => SetProperty(UseUnderlineProperty, value);
        }

        #endregion

        #region Signals

        public static readonly Signal ActivateSignal = Signal.Wrap("activate");

        public event EventHandler<SignalArgs> OnActivate
        {
            add => ActivateSignal.Connect(this, value, true);
            remove => ActivateSignal.Disconnect(this, value);
        }

        public static readonly Signal ClickedSignal = Signal.Wrap("clicked");

        public event EventHandler<SignalArgs> OnClicked
        {
            add => ClickedSignal.Connect(this, value, true);
            remove => ClickedSignal.Disconnect(this, value);
        }

        #endregion
        
        public Button(string label)
            : this(
                ConstructParameter.With(LabelProperty, label)
            )
        { }
    }
}