using System;
using GObject.Core;

namespace Gtk.Core
{
    public class GEventBox : GBin
    {
        protected Property<bool> AboveChild { get; }
        protected Property<bool> VisibleWindow { get; }

        internal protected GEventBox(IntPtr handle) : base(handle) 
        {
            AboveChild = Property<bool>("above-child",
                get : GetBool,
                set : Set
            );

            VisibleWindow = Property<bool>("visible-window",
                get : GetBool,
                set : Set
            );
        }
    }
}