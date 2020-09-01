using System;
using GObject;

namespace Gtk
{
    public partial class Button
    {
        public event EventHandler<EventArgs>? Clicked;

        public Button(string label) : this(Sys.Button.new_with_label(label)) { }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}