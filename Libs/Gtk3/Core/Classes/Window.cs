using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
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