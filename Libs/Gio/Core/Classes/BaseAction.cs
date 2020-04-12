using System;
using GObject.Core;

namespace Gio.Core
{
    public abstract class GBaseAction : GObject.Core.GObject
    {
        public Property<bool> Enabled { get; }
        public Property<string> Name { get; }

        public GBaseAction(string name) : this(Gio.SimpleAction.@new(name, IntPtr.Zero)) { }
        internal GBaseAction(IntPtr handle) : base(handle)
        {
            Enabled = PropertyOfBool("enabled");
            Name = PropertyOfString("name");

            RegisterEvent("activate", OnActivate);
        }

        protected abstract void OnActivate();
    }
}