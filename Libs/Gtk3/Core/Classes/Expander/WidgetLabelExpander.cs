using System;
using GObject;

namespace Gtk
{
    public class WidgetLabelExpander : Expander
    {
        public Property<Widget> Widget { get; }

        public WidgetLabelExpander(Widget widget) : this(Sys.Expander.@new("default"), widget) { }
        internal WidgetLabelExpander(IntPtr handle, Widget widget) : base(handle)
        {
            Widget = Property("label-widget",
                get: GetObject<Widget>,
                set: Set
            );

            Widget.Value = widget;
        }
    }
}