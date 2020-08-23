using System;
using GObject;

namespace Gtk
{
    public partial class ToggleButton
    {
        public event EventHandler<EventArgs>? Toggled;

        public Property<bool> Active { get; }

        public ToggleButton(string text) : this(Sys.ToggleButton.new_with_label(text)) {}

        protected virtual void OnToggled() => Toggled?.Invoke(this, EventArgs.Empty);
    }
}