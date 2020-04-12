using System;
using GObject.Core;

namespace Gtk.Core
{
    public abstract class GExpander : GBin
    {
        public Property<bool> Expanded { get; }
        public Property<bool> ResizeToplevel { get; }
        public Property<bool> LabelFill { get; }

        internal GExpander(IntPtr handle) : base(handle)
        {
            Expanded = PropertyOfBool("expanded");
            ResizeToplevel = PropertyOfBool("resize-toplevel");
            LabelFill = PropertyOfBool("label-fill");
        }
    }
}