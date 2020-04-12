using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GButton : GWidget
    {
        public event EventHandler<EventArgs>? Clicked;

        public Property<string> Text { get; }

        public GButton(string text) : this(Gtk.Button.new_with_label(text)) {}
        internal GButton(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");

            RegisterEvent("clicked", OnClicked);
        }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}