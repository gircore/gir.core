using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GEventBox : GBin
    {
        protected Property<bool> AboveChild { get; }
        protected Property<bool> VisibleWindow { get; }

        internal protected GEventBox(IntPtr handle) : base(handle) 
        {
            AboveChild = PropertyOfBool("above-child");
            VisibleWindow = PropertyOfBool("visible-window");
        }
    }
}