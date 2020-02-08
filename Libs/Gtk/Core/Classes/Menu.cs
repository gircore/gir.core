using System;
using System.Reflection;

namespace Gtk.Core
{
    public class GMenu : Gio.Core.GMenu
    {
        private GBuilder? builder;

        public GMenu(string template, string obj = "menu") : this(new GBuilder(template, Assembly.GetCallingAssembly()), obj) {}

        internal GMenu(GBuilder builder, string obj) : this(builder.GetObject(obj))
        {
            this.builder = builder;
        }

        internal GMenu(IntPtr handle) : base(handle) { }
    }
}