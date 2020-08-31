using System.Reflection;

namespace Gtk
{
    public partial class Widget : IWidget
    {
        private Builder? builder;

        internal Widget(string template, string obj, Assembly assembly) : this(new Builder(template, assembly), obj) { }

        internal Widget(Builder builder, string obj) : this(builder.GetObject(obj))
        {
            this.builder = builder;
            builder.Connect(this);
        }

        public void Show() => Sys.Widget.show(Handle);
        public void ShowAll() => Sys.Widget.show_all(Handle);
    }
}