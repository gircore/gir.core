using System;

namespace Gio
{
    public class SimpleAction : BaseAction
    {
        public event EventHandler<EventArgs>? Activate;

        public SimpleAction(string name) : base(name) { }

        protected override void OnActivate() => Activate?.Invoke(this, EventArgs.Empty);
    }
}