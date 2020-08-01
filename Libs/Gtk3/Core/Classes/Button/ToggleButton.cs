using System;
using GObject;

namespace Gtk
{
    public class ToggleButton : Button
    {
        public event EventHandler<EventArgs>? Toggled;

        public Property<bool> Active { get; }

        public ToggleButton(string text) : this(Sys.ToggleButton.new_with_label(text)) {}

        internal ToggleButton(IntPtr handle) : base(handle)
        {
            Active = PropertyOfBool("active");

            RegisterEvent("toggled", OnToggled);
        }

        protected virtual void OnToggled() => Toggled?.Invoke(this, EventArgs.Empty);
    }
}