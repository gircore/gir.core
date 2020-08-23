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
    }
}