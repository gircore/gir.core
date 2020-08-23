using System;
using System.Reflection;

namespace Gtk
{
    public partial class ApplicationWindow
    {
        public ApplicationWindow(Application application) : this(Sys.ApplicationWindow.@new(application.Handle)) {}
        public ApplicationWindow(Application application, string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) 
        {
            Application.Value = application;
        }
        internal ApplicationWindow(IntPtr handle) : base(handle) {}
    }
}