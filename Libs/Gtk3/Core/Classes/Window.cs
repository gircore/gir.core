using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
        private Property<int> defaultHeight;
        public Property<int> DefaultHeight => defaultHeight;

        private Property<int> defaultWith;
        public Property<int> DefaultWidth => defaultWith;

        private Property<Application?> application;
        public Property<Application?> Application => application;

        #endregion Properties

        public Window() : this(Sys.Window.@new(Sys.WindowType.toplevel)) {}
        public Window(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) 
        {
            InitProperties(out defaultHeight, out defaultWith, out application);
        }
        internal Window(string template, string obj, Assembly assembly) : base(template, obj, assembly) 
        {
            InitProperties(out defaultHeight, out defaultWith, out application);
        }
        internal Window(IntPtr handle) : base(handle) 
        {
            InitProperties(out defaultHeight, out defaultWith, out application);
        }

        private void InitProperties(out Property<int> defaultHeight, out Property<int> defaultWidth, out Property<Application?> application)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            application = Property<Application?>("application",
                get : GetObject<Application?>,
                set: Set
            );
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));
    }
}