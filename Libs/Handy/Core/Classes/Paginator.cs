using GObject.Core;
using Gtk.Core;
using System;

namespace Handy.Core
{
    public class GPaginator : GEventBox
    {
        #region Properties
        public Property<uint> AnimationDuration { get; }
        public Property<uint> Pages { get; }
        #endregion Properties

        public GPaginator() : this(Handy.Paginator.@new()){}

        internal GPaginator(IntPtr handle) : base(handle) 
        { 
            AnimationDuration = Property<uint>("animation-duration",
                get : GetUInt,
                set : Set
            );

            Pages = Property<uint>("n-pages",
                get : GetUInt,
                set : Set
            );
        }

        public void Prepend(GWidget widget) => Handy.Paginator.prepend(this, widget);
        public void Append(GWidget widget) => Handy.Paginator.insert(this, widget, -1);
        public void ScrollTo(GWidget widget) => Handy.Paginator.scroll_to(this, widget);
    }
}