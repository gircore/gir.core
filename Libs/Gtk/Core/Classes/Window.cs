using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public class GWindow : GContainer
    {
        #region Properties
        public Property<int> DefaultHeight { get; private set;} = default!;
        public Property<int> DefaultWidth { get; private set; } = default!;
        public Property<GApplication?> Application {get; private set; } = default!;

        #endregion Properties

        public GWindow() : this(Gtk.Window.@new(Gtk.WindowType.toplevel)) {}
        public GWindow(string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) 
        {
            InitProperties();
        }
        internal GWindow(string template, string obj, Assembly assembly) : base(template, obj, assembly) 
        {
            InitProperties();
        }
        internal GWindow(IntPtr handle) : base(handle) 
        {
            InitProperties();
        }

        private void InitProperties()
        {
            DefaultHeight = Property<int>("default-height",
                get: GetInt,
                set: Set
            );

            DefaultWidth = Property<int>("default-width",
                get: GetInt,
                set: Set
            );

            Application = Property<GApplication?>("application",
                get : GetObject<GApplication?>,
                set: Set
            );
        }

        public void SetDefaultSize(int width, int height) => Gtk.Window.set_default_size(this, width, height);
        public void SetTitlebar(GWidget widget) => Gtk.Window.set_titlebar(this, widget);
    }
}