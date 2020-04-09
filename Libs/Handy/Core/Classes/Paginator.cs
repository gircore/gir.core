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
            AnimationDuration = Property<uint>("animation-duration",
                get : GetUInt,
                set : Set
            );

            Pages = Property<uint>("n-pages",
                get : GetUInt,
                set : Set
            );

            AllowMouseDrag = Property<bool>("allow-mouse-drag",
                get : GetBool,
                set : Set
            );

            Position = ReadOnlyProperty<double>("position",
                get : GetDouble
            );

            Spacing = Property<uint>("spacing",
                get : GetUInt,
                set : Set 
            );

            Interactive = Property<bool>("interactive",
                get : GetBool,
                set : Set
            );

            IndicatorSpacing = Property<uint>("indicator-spacing",
                get : GetUInt,
                set : Set
            );

            CenterContent = Property<bool>("center-content",
                get : GetBool,
                set : Set
            );

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