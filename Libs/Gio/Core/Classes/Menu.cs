using System;

namespace Gio.Core
{
    public class GMenu : GObject.Core.GObject
    {
        public GMenu() : this(Gio.Menu.@new()) { }
        protected internal GMenu(IntPtr handle) : base(handle)
        {
        }
    }
}