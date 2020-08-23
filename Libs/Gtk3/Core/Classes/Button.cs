using System;
using GObject;

namespace Gtk
{
    public partial class Button
    {
        public event EventHandler<EventArgs>? Clicked;

        public Property<string> Text { get; }

        public Button(string text) : this(Sys.Button.new_with_label(text)) {}
        internal Button(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");

            RegisterEvent("clicked", OnClicked);
        }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}