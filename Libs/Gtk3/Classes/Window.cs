using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
        
        public static readonly Property<Application> ApplicationProperty = Property<Application>.Register<Window>(
            Native.ApplicationProperty,
            nameof(Application),
            get: (o) => o.Application,
            set: (o, v) => o.Application = v
        );

        public Application Application
        {
            get => GetProperty(ApplicationProperty);
            set => SetProperty(ApplicationProperty, value);
        }

        public static readonly Property<bool> ResizableProperty = Property<bool>.Register<Window>(
            Native.ResizableProperty,
            nameof(Resizable),
            get: (o) => o.Resizable,
            set: (o, v) => o.Resizable = v
        );

        public bool Resizable
        {
            get => GetProperty(ResizableProperty);
            set => SetProperty(ResizableProperty, value);
        }

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

        #region Constructors

        public Window(string title)
            : this(
               ConstructParameter.With(TitleProperty, title)
            )
        { }

        #endregion
        
        #region Methods
        
        public void SetTitle(string title) => Native.set_title(Handle, title);
        public void Close() => Native.close(Handle);
        
        #endregion
    }
}
