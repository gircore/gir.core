using GObject;
using Gtk;
using System;

namespace Handy
{
    public partial class Paginator
    {
        public event EventHandler<PageChangedEventArgs>? PageChanged;

        #region Properties
        public IProperty<uint> AnimationDuration { get; }
        public IProperty<uint> Pages { get; }
        public IProperty<bool> AllowMouseDrag { get; }
        public ReadOnlyProperty<double> Position { get; }
        public IProperty<uint> Spacing { get; }
        public IProperty<bool> Interactive { get; }
        public IProperty<uint> IndicatorSpacing { get; }
        public IProperty<bool> CenterContent { get; }
        public IProperty<PaginatorIndicatorStyle> IndicatorStyle { get; }
        #endregion Properties

        public Paginator() : this(Sys.Paginator.@new()) { }

        public void Prepend(Widget widget) => Sys.Paginator.prepend(Handle, GetHandle(widget));
        public void Append(Widget widget) => Sys.Paginator.insert(Handle, GetHandle(widget), -1);
        public void ScrollTo(Widget widget) => Sys.Paginator.scroll_to(Handle, GetHandle(widget));

        protected void OnPageChanged(ref GObject.Sys.Value[] values)
        {
            var index = GObject.Sys.Value.get_uint(ref values[1]);
            PageChanged?.Invoke(this, new PageChangedEventArgs(index));
        }
    }
}