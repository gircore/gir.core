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
            Expanded = Property<bool>("expanded",
                get: GetBool,
                set: Set
            );

            ResizeToplevel = Property<bool>("resize-toplevel",
                get: GetBool,
                set: Set
            );

            LabelFill = Property<bool>("label-fill",
                get: GetBool,
                set: Set
            );
        }
    }
}