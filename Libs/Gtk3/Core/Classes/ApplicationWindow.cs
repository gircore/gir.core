using System;
using System.Reflection;

namespace Gtk
{
    public partial class ApplicationWindow
    {
        //TODO: constructor parameters are not intuitive
        public ApplicationWindow(Application application, string template, string obj = "root") : base(template, obj, Assembly.GetCallingAssembly()) 
        {
            Application.Value = application;
        }
    }
}