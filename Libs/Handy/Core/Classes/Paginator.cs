using GObject;
using GObject.Core;
using Gtk.Core;
using System;

namespace Handy.Core
{
    public class GPaginator : GEventBox
    {
        public event EventHandler<PageChangedEventArgs>? PageChanged;
        #region Properties
        public Property<uint> AnimationDuration { get; }
        public Property<uint> Pages { get; }
        public Property<bool> AllowMouseDrag { get; }
        public ReadOnlyProperty<double> Position { get; }
        public Property<uint> Spacing { get; }
        public Property<bool> Interactive { get; }
        public Property<uint> IndicatorSpacing { get; }
        public Property<bool> CenterContent { get; }
        public Property<PaginatorIndicatorStyle> IndicatorStyle { get; }
        #endregion Properties

        public GPaginator() : this(Handy.Paginator.@new()){}

        internal GPaginator(IntPtr handle) : base(handle) 
        { 
            AnimationDuration = PropertyOfUint("animation-duration");
            Pages = PropertyOfUint("n-pages");
            AllowMouseDrag = PropertyOfBool("allow-mouse-drag");
            Position = ReadOnlyPropertyOfDouble("position");
            Spacing = PropertyOfUint("spacing");
            Interactive = PropertyOfBool("interactive");
            IndicatorSpacing = PropertyOfUint("indicator-spacing");
            CenterContent = PropertyOfBool("center-content");

            IndicatorStyle = Property<PaginatorIndicatorStyle>("indicator-style",
                get : GetEnum<PaginatorIndicatorStyle>,
                set : SetEnum<PaginatorIndicatorStyle>
            );

            RegisterEvent("page-changed", OnPageChanged);
        }

        public void Prepend(GWidget widget) => Handy.Paginator.prepend(this, widget);
        public void Append(GWidget widget) => Handy.Paginator.insert(this, widget, -1);
        public void ScrollTo(GWidget widget) => Handy.Paginator.scroll_to(this, widget);

        protected void OnPageChanged(ref Value[] values)
        { 
            var index = (uint) values[1];
            PageChanged?.Invoke(this, new PageChangedEventArgs(index));
        }
    }
}