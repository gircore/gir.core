using GObject;
using Gtk;
using System;

namespace Handy
{
    public class Paginator : EventBox
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

        public Paginator() : this(Sys.Paginator.@new()){}

        internal Paginator(IntPtr handle) : base(handle) 
        { 
            AnimationDuration = PropertyOfUint("animation-duration");
            Pages = PropertyOfUint("n-pages");
            AllowMouseDrag = PropertyOfBool("allow-mouse-drag");
            Position = ReadOnlyPropertyOfDouble("position");
            Spacing = PropertyOfUint("spacing");
            Interactive = PropertyOfBool("interactive");
            IndicatorSpacing = PropertyOfUint("indicator-spacing");
            CenterContent = PropertyOfBool("center-content");

            IndicatorStyle = Property("indicator-style",
                get : GetEnum<PaginatorIndicatorStyle>,
                set : SetEnum
            );

            RegisterEvent("page-changed", OnPageChanged);
        }

        public void Prepend(Widget widget) => Sys.Paginator.prepend(this, widget);
        public void Append(Widget widget) => Sys.Paginator.insert(this, widget, -1);
        public void ScrollTo(Widget widget) => Sys.Paginator.scroll_to(this, widget);

        protected void OnPageChanged(ref GObject.Sys.Value[] values)
        { 
            var index = (uint) values[1];
            PageChanged?.Invoke(this, new PageChangedEventArgs(index));
        }
    }
}