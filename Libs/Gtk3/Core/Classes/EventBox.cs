using System;
using GObject;

namespace Gtk
{
    public partial class EventBox
    {
        #region Properties
        protected IProperty<bool> AboveChild { get; }
        protected IProperty<bool> VisibleWindow { get; }
        #endregion Properties
    }
}