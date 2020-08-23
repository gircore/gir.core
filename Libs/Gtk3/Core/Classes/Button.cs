using System;
using GObject;

namespace Gtk
{
    public partial class Button
    {
        public event EventHandler<EventArgs>? Clicked;

        public IProperty<string> Text { get; }

        public Button(string text) : this(Sys.Button.new_with_label(text)) { }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}