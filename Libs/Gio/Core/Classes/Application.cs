using System;
using System.Collections.Generic;
using GObject;

namespace Gio
{
    public partial class Application : GObject.Object
    {
        public event EventHandler<EventArgs>? Activate;
        public event EventHandler<EventArgs>? Startup;

        public Property<string> ApplicationId { get; set; }
       
        public Application(string applicationId) : this(Sys.Application.@new(applicationId, Sys.ApplicationFlags.flags_none)) {}

        internal protected Application(IntPtr handle) : base(handle)
        {
            actions = new Dictionary<string, CommandAction>();

            ApplicationId = PropertyOfString("application-id");

            RegisterEvent("activate", OnActivated);
            RegisterEvent("startup", OnStartup);
        }

        public void Run()
        {
            var zero = IntPtr.Zero;
            Sys.Application.run(this, 0, ref zero);
        }

        protected virtual void OnStartup() => Startup?.Invoke(this, EventArgs.Empty);
        protected virtual void OnActivated() => Activate?.Invoke(this, EventArgs.Empty);
    }
}