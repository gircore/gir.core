using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GApplicationWindow : GWindow
    {
        public GApplicationWindow(GApplication application) : this(Gtk.ApplicationWindow.@new(application)) {}
        public GApplicationWindow(GApplication application, string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) 
        {
            Application.Value = application;
        }
        internal GApplicationWindow(IntPtr handle) : base(handle) {}
    }
}