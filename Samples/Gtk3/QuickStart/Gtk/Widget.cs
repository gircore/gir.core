using System;
using GObject;

namespace Gtk
{
    public partial class Widget : InitiallyUnowned
    {
        #region Methods

        public void Show() => Sys.Widget.show(Handle);
        public void ShowAll() => Sys.Widget.show_all(Handle);

        #endregion

        #region Experimental

        public EventHandler<SignalArgs> this[Signal signal]
        {
            set => signal.Connect(this, value, true);
        }

        #endregion
    }
}
