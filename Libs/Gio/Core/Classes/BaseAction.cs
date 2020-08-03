using System;
using GObject;

namespace Gio
{
    public abstract class BaseAction : GObject.Object
    {
        public Property<bool> Enabled { get; }
        public Property<string> Name { get; }

        public BaseAction(string name) : this(Sys.SimpleAction.@new(name, IntPtr.Zero)) { }
        internal BaseAction(IntPtr handle) : base(handle)
        {
            Enabled = PropertyOfBool("enabled");
            Name = PropertyOfString("name");

            RegisterEvent("activate", OnActivate);
        }

        protected abstract void OnActivate();
    }
}