using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public class GWindow : GWidget
    {
        #region Properties
        private Property<int> defaultHeight;
        public Property<int> DefaultHeight => defaultHeight;

        private Property<int> defaultWidth;
        public Property<int> DefaultWidth => defaultWidth;

        private Property<GApplication?> application;
        public Property<GApplication?> Application => application;

        #endregion Properties

        public GWindow() : this(Gtk.Window.@new()) {}
        internal GWindow(IntPtr handle) : base(handle) 
        {
            InitProperties(out defaultHeight, out defaultWidth, out application);
        }

        private void InitProperties(out Property<int> defaultHeight, out Property<int> defaultWidth, out Property<GApplication?> application)
        {
            defaultHeight = PropertyOfInt("default-height");
            defaultWidth = PropertyOfInt("default-width");

            application = Property<GApplication?>("application",
                get : GetObject<GApplication?>,
                set: Set
            );
        }

        public void SetDefaultSize(int width, int height) => Gtk.Window.set_default_size(this, width, height);
        public void SetTitlebar(GWidget widget) => Gtk.Window.set_titlebar(this, widget);
        public void SetChild(GWidget child) => Gtk.Window.set_child(this, child);
    }
}