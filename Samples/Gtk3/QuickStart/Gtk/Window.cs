using System;
using System.Reflection;
using GObject;

namespace Gtk
{
    public class Window : Container
    {
        #region Properties
        private Property<int> defaultHeight;
        public Property<int> DefaultHeight => defaultHeight;

        private Property<int> defaultWith;
        public Property<int> DefaultWidth => defaultWith;

        //private Property<Application?> application;
        //public Property<Application?> Application => application;

        internal new static GObject.Sys.Type GetGType() => new GObject.Sys.Type(Sys.Window.get_type());

        #endregion Properties

        public Window(string title) : base(GObject.ConstructProp.With("title", title))
        {
            InitProperties(out defaultHeight, out defaultWith);
        }

        protected Window(params ConstructProp[] properties) : base(properties)
        {
            InitProperties(out defaultHeight, out defaultWith);
        }

        internal Window(IntPtr handle) : base(handle) 
        {
            InitProperties(out defaultHeight, out defaultWith);
        }

        private void InitProperties(out Property<int> defaultHeight, out Property<int> defaultWidth)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            /*application = Property<Application?>("application",
                get : GetObject<Application?>,
                set: Set
            );*/
        }

        public void SetDefaultSize(int width, int height) => Sys.Window.set_default_size(this.Handle, width, height);
        public void SetTitlebar(Widget widget) => Sys.Window.set_titlebar(this.Handle, widget.Handle);
    }
}