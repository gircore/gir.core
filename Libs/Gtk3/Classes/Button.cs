using System;
using GObject;

namespace Gtk
{
    public partial class Button
    {
        //TODO: Workaround as long as typedict is not filled
        public static GObject.Type Bla() => GTypeDescriptor.GType;
        
        private static readonly unsafe delegate*<nint, void> _originalPressed;
        
        #region Properties

        public static readonly Property<bool> AlwaysShowImageProperty = Property<bool>.Register<Button>(
            Native.AlwaysShowImageProperty,
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
            Native.ImageProperty,
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
            Native.ImagePositionProperty,
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

        public static readonly Property<ReliefStyle> ReliefProperty = Property<ReliefStyle>.Register<Button>(
            Native.ReliefProperty,
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

        #endregion

        //TODO: Remove this testcode
        static Button()
        {
            var gtype = GTypeDescriptor.GType;
            var ptr = gtype.GetClassPointer();

            unsafe
            {
                var btnClass = (ButtonClass*) ptr;
                _originalPressed = btnClass->pressed;
                btnClass->pressed = &PrivatePressed;
            }
        }

        private static unsafe void PrivatePressed(nint instance)
        {
            if (GetObject<Button>(instance, out var b))
                b.Pressed();
            else
                _originalPressed(instance);
        }
        protected internal virtual void Pressed()
        {
            unsafe
            {
                _originalPressed(Handle);
            }
        }

        public Button(string label)
            : this(
                ConstructParameter.With(LabelProperty, label)
            )
        { }
    }
}
