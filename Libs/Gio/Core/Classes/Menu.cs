using System;

namespace Gio
{
    public class Menu : GObject.Object
    {
        public Menu() : this(Sys.Menu.@new()) { }
        protected internal Menu(IntPtr handle) : base(handle)
        {
        }
    }
}