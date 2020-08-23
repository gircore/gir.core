using System;
using GObject;

namespace Gtk
{
    public partial class Window
    {
        #region Properties
        private Property<int> defaultHeight = null!;//TODO
        public Property<int> DefaultHeight => defaultHeight;

        private Property<int> defaultWidth = null!;//TODO
        public Property<int> DefaultWidth => defaultWidth;

        private Property<Application?> application = null!;//TODO
        public Property<Application?> Application => application;

        #endregion Properties

        public Window() : this(Sys.Window.@new()) {}

        private void InitProperties(out Property<int> defaultHeight, out Property<int> defaultWidth, out Property<Application?> application)
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