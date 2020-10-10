using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties

        public static readonly Property<string> TitleProperty = Property<string>.Register<Window>(
            Native.TitleProperty,
            nameof(Title),
            get: (o) => o.Title,
            set: (o, v) => o.Title = v
        );

        public string Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        public static readonly Property<int> DefaultHeightProperty = Property<int>.Register<Window>(
            Native.DefaultHeightProperty,
            nameof(DefaultHeight),
            get: (o) => o.DefaultHeight,
            set: (o, v) => o.DefaultHeight = v
        );

        public int DefaultHeight
        {
            get => GetProperty(DefaultHeightProperty);
            set => SetProperty(DefaultHeightProperty, value);
        }

        public static readonly Property<int> DefaultWidthProperty = Property<int>.Register<Window>(
            Native.DefaultWidthProperty,
            nameof(DefaultWidth),
            get: (o) => o.DefaultWidth,
            set: (o, v) => o.DefaultWidth = v
        );

        public int DefaultWidth
        {
            get => GetProperty(DefaultWidthProperty);
            set => SetProperty(DefaultWidthProperty, value);
        }

        #endregion Properties

        #region Constructors

        public Window(string title)
            : this(
               ConstructParameter.With(TitleProperty, title)
            )
        { }

        #endregion
        public void SetTitle(string title) => Native.set_title(Handle, title);
        public void Close() => Native.close(Handle);
    }
}