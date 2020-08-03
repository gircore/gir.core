using System;
using GObject;

namespace Gtk
{
    public class EventBox : Bin
    {
        protected Property<bool> AboveChild { get; }
        protected Property<bool> VisibleWindow { get; }

        internal protected EventBox(IntPtr handle) : base(handle) 
        {
            AboveChild = PropertyOfBool("above-child");
            VisibleWindow = PropertyOfBool("visible-window");
        }
    }
}