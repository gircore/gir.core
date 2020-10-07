using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties

        public static readonly Property<string> TitleProperty = GObject.Property<string>.Register<Window>(
            "title",
            nameof(Title),
            get: (o) => o.Title,
            set: (o, v) => o.Title = v
        );

        public string Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        public static readonly Property<int> DefaultHeightProperty = GObject.Property<int>.Register<Window>(
            "default-height",
            nameof(DefaultHeight),
            get: (o) => o.DefaultHeight,
            set: (o, v) => o.DefaultHeight = v
        );

        public int DefaultHeight
        {
            get => GetProperty(DefaultHeightProperty);
            set => SetProperty(DefaultHeightProperty, value);
        }

        public static readonly Property<int> DefaultWidthProperty = GObject.Property<int>.Register<Window>(
            "default-width",
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
        public void SetTitle(string title) => Window.set_title(Handle, title);
        public void ShowAll() => Window.show_all(Handle);
        public void Close() => Window.close(Handle);
    }
}