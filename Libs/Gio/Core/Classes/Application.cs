using System;
using System.Collections.Generic;
using GObject;

namespace Gio
{
    public partial class Application
    {
        public event EventHandler<EventArgs>? Activate;
        public event EventHandler<EventArgs>? Startup;

        public Application(string applicationId) : this(Sys.Application.@new(applicationId, Sys.ApplicationFlags.flags_none)) { }

        public void Run()
        {
            var zero = IntPtr.Zero;
            Sys.Application.run(Handle, 0, ref zero);
        }

        protected virtual void OnStartup() => Startup?.Invoke(this, EventArgs.Empty);
        protected virtual void OnActivated() => Activate?.Invoke(this, EventArgs.Empty);
    }
}