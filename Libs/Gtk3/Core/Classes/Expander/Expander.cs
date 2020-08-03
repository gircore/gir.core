using System;
using GObject;

namespace Gtk
{
    public abstract class Expander : Bin
    {
        public Property<bool> Expanded { get; }
        public Property<bool> ResizeToplevel { get; }
        public Property<bool> LabelFill { get; }

        internal Expander(IntPtr handle) : base(handle)
        {
            Expanded = PropertyOfBool("expanded");
            ResizeToplevel = PropertyOfBool("resize-toplevel");
            LabelFill = PropertyOfBool("label-fill");
        }
    }
}