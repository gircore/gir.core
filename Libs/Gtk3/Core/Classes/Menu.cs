using System;
using System.Reflection;

namespace Gtk
{
    public partial class Menu
    {
        private Builder? builder;

        public Menu(string template, string obj = "menu") : this(new Builder(template, Assembly.GetCallingAssembly()), obj) {}

        internal Menu(Builder builder, string obj) : this(builder.GetObject(obj))
        {
            this.builder = builder;
        }

        internal Menu(IntPtr handle) : base(handle) { }
    }
}