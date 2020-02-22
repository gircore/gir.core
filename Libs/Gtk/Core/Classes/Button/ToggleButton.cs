using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GToggleButton : GButton
    {
        public event EventHandler<EventArgs>? Toggled;

        public Property<bool> Active { get; }

        public GToggleButton(string text) : this(Gtk.ToggleButton.new_with_label(text)) {}

        internal GToggleButton(IntPtr handle) : base(handle)
        {
            Active = Property<bool>("active",
                get: GetBool, 
                set: Set
            );

            RegisterEvent("toggled", OnToggled);
        }

        protected virtual void OnToggled() => Toggled?.Invoke(this, EventArgs.Empty);
    }
}