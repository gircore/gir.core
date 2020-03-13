using System;
using GObject.Core;

namespace Gtk.Core
{
    public class WidgetLabelExpander : GExpander
    {
        public Property<GWidget> Widget { get; } = default!;

        public WidgetLabelExpander(GWidget widget) : this(Gtk.Expander.@new("default"), widget) { }
        internal WidgetLabelExpander(IntPtr handle, GWidget widget) : base(handle)
        {
            Widget = Property<GWidget>("label-widget",
                get: GetObject<GWidget>,
                set: Set
            );

            Widget.Value = widget;
        }
    }
}