using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties

        public static readonly Property<string> TitleProperty = Property<string>.Register<Window>(
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

        public static readonly Property<int> DefaultHeightProperty = Property<int>.Register<Window>(
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

        public static readonly Property<int> DefaultWidthProperty = Property<int>.Register<Window>(
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
                ConstructParam.With(TitleProperty, title)
            )
        { }

        #endregion

        #region Methods

        public void Close() => Sys.Window.close(Handle);
        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));

        #endregion
    }
}
