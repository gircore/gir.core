using System;
using GObject;

namespace Gtk
{
    public partial class EventBox
    {
        #region Properties
        protected Property<bool> AboveChild { get; }
        protected Property<bool> VisibleWindow { get; }
        #endregion Properties

        internal protected EventBox(IntPtr handle) : base(handle) 
        {
            AboveChild = PropertyOfBool("above-child");
            VisibleWindow = PropertyOfBool("visible-window");
        }
    }
}