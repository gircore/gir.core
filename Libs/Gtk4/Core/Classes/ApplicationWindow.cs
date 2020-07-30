using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GApplicationWindow : GWindow
    {
        public GApplicationWindow(GApplication application) : this(Gtk.ApplicationWindow.@new(application)) {}
        internal GApplicationWindow(IntPtr handle) : base(handle) {}
    }
}