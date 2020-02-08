using System;
using System.Collections.Generic;
using GObject.Core;

namespace Gio.Core
{
    public partial class GApplication : GObject.Core.GObject
    {
        public event EventHandler<EventArgs>? Activate;
        public event EventHandler<EventArgs>? Startup;

        public Property<string> ApplicationId { get; set; }
       
        public GApplication(string applicationId) : this(Application.@new(applicationId, ApplicationFlags.flags_none)) {}

        internal protected GApplication(IntPtr handle) : base(handle)
        {
            actions = new Dictionary<string, GCommandAction>();

            ApplicationId = Property<string>("application-id",
                get : GetStr,
                set : Set
            );

            RegisterEvent("activate", OnActivated);
            RegisterEvent("startup", OnStartup);
        }

        public void Run()
        {
            var zero = IntPtr.Zero;
            Application.run(this, 0, ref zero);
        }

        protected virtual void OnStartup() => Startup?.Invoke(this, EventArgs.Empty);
        protected virtual void OnActivated() => Activate?.Invoke(this, EventArgs.Empty);
    }
}