using System;

namespace Gio.Core
{
    public class GSimpleAction : GBaseAction
    {
        public event EventHandler<EventArgs>? Activate;

        public GSimpleAction(string name) : base(name) { }

        protected override void OnActivate() => Activate?.Invoke(this, EventArgs.Empty);
    }
}