using System;
using GObject;

namespace Gio
{
    public partial class SimpleAction
    {
        public Property<bool> Enabled { get; }
        public Property<string> Name { get; }

        public SimpleAction(string name) : this(Sys.SimpleAction.@new(name, IntPtr.Zero)) { }
    }
}