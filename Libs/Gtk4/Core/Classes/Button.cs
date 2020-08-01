using System;
using GObject;

namespace Gtk
{
    public class Button : Widget
    {
        public event EventHandler<EventArgs>? Clicked;

        public Property<string> Text { get; }

        public Button() : this(Sys.Button.@new()) {}
        public Button(string text) : this(Sys.Button.new_with_label(text)) {}
        internal Button(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");

            RegisterEvent("clicked", OnClicked);
        }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}