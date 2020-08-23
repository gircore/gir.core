using System;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
        private IProperty<int> defaultHeight;
        public IProperty<int> DefaultHeight => defaultHeight;

        private IProperty<int> defaultWidth;
        public IProperty<int> DefaultWidth => defaultWidth;

        private IProperty<Application?> application;
        public IProperty<Application?> Application => application;

        #endregion Properties

        public Window() : this(Sys.Window.@new()) { }

        private void InitProperties(out IProperty<int> defaultHeight, out IProperty<int> defaultWidth, out IProperty<Application?> application)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            //TODO
            /*application = Property("application",
                get : GetObject<Application?>,
                set: Set
            );*/
            application = null!;
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(Handle, GetHandle(widget));
        public void SetChild(Widget child) => Sys.Window.set_child(Handle, GetHandle(child));
    }
}