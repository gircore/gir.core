using System;
using GObject;

namespace Gtk
{
    public class Window : Widget
    {
        #region Properties
        private Property<int> defaultHeight;
        public Property<int> DefaultHeight => defaultHeight;

        private Property<int> defaultWidth;
        public Property<int> DefaultWidth => defaultWidth;

        private Property<Application?> application;
        public Property<Application?> Application => application;

        #endregion Properties

        public Window() : this(Sys.Window.@new()) {}
        internal Window(IntPtr handle) : base(handle) 
        {
            InitProperties(out defaultHeight, out defaultWidth, out application);
        }

        private void InitProperties(out Property<int> defaultHeight, out Property<int> defaultWidth, out Property<Application?> application)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            application = Property("application",
                get : GetObject<Application?>,
                set: Set
            );
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(this, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(this, widget);
        public void SetChild(Widget child) => Sys.Window.set_child(this, child);
    }
}