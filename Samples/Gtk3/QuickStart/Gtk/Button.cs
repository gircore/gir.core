using System;
using GObject;

namespace Gtk
{
    public partial class Button : Widget
    {
        public event EventHandler<EventArgs>? Clicked;

        public Property<string> Text { get; }

        internal new static GObject.Sys.Type GetGType() => new GObject.Sys.Type(Sys.Button.get_type());

        public Button(string text) : base(ConstructProp.With("label", text))
        {
            Text = PropertyOfString("label");

            RegisterEvent("clicked", OnClicked);
        }

        internal Button(IntPtr handle) : base(handle) 
        {
            Text = PropertyOfString("label");

            RegisterEvent("clicked", OnClicked);
        }

        protected virtual void OnClicked() => Clicked?.Invoke(this, EventArgs.Empty);
    }
}