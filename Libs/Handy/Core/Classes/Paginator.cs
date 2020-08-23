using GObject;
using Gtk;
using System;

namespace Handy
{
    public partial class Paginator
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

        public void Prepend(Widget widget) => Sys.Paginator.prepend(Handle, GetHandle(widget));
        public void Append(Widget widget) => Sys.Paginator.insert(Handle, GetHandle(widget), -1);
        public void ScrollTo(Widget widget) => Sys.Paginator.scroll_to(Handle, GetHandle(widget));

        protected void OnPageChanged(ref GObject.Sys.Value[] values)
        { 
            var index = (uint) values[1];
            PageChanged?.Invoke(this, new PageChangedEventArgs(index));
        }
    }
}