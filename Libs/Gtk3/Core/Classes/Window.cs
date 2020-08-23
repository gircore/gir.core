using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
        public static readonly Property<int> DefaultHeightProperty = GObject.Property<int>.Register<Window>("default-height", get: (o) => o.DefaultHeightEx, set: (o, v) => o.DefaultHeightEx = v);

        public int DefaultHeightEx
        {
            get => GetProperty(DefaultHeightProperty);
            set => SetProperty(DefaultHeightProperty, value);
        }

        public static readonly Property<int> DefaultWidthProperty = GObject.Property<int>.Register<Window>("default-width", get: (o) => o.DefaultWidthEx, set: (o, v) => o.DefaultWidthEx = v);

        public int DefaultWidthEx
        {
            get => GetProperty(DefaultWidthProperty);
            set => SetProperty(DefaultWidthProperty, value);
        }

        public static readonly Property<Application> ApplicationProperty = GObject.Property<Application>.Register<Window>("application", get: (o) => o.ApplicationEx, set: (o, v) => o.ApplicationEx = v);

        public Application ApplicationEx
        {
            get => GetProperty(ApplicationProperty);
            set => SetProperty(ApplicationProperty, value);
        }

        private IProperty<int> defaultHeight;
        public IProperty<int> DefaultHeight => defaultHeight;

        private IProperty<int> defaultWith;
        public IProperty<int> DefaultWidth => defaultWith;

        private IProperty<Application?> application;
        public IProperty<Application?> Application => application;

        #endregion Properties

        public Window() : this(Sys.Window.@new(Sys.WindowType.toplevel)) { }
        public Window(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly())
        {
            InitProperties(out defaultHeight, out defaultWith, out application);
        }
        internal Window(string template, string obj, Assembly assembly) : base(template, obj, assembly)
        {
            InitProperties(out defaultHeight, out defaultWith, out application);
        }

        private void InitProperties(out IProperty<int> defaultHeight, out IProperty<int> defaultWidth, out IProperty<Application?> application)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            /*application = Property("application",
                get : GetObject<Application?>,
                set: Set
            );*/
            application = null!;
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));
    }
}