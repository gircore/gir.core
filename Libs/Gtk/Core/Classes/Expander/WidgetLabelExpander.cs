using System;
using GObject.Core;

namespace Gtk.Core
{
    public class WidgetLabelExpander : GExpander
    {
        private GWidget widget = default!;

        public Property<GWidget> Widget { get; } = default!;

        public WidgetLabelExpander(GWidget widget) : this(Gtk.Expander.@new("default"), widget) { }
        internal WidgetLabelExpander(IntPtr handle, GWidget widget) : base(handle)
        {
            Widget = Property<GWidget>("label-widget",
                get: GetWidget,
                set: SetWidget
            );

            Widget.Value = widget;
        }

        private GWidget GetWidget(string propertyName) => widget;

        private void SetWidget(GWidget widget, string propertyName)
        {
            Set(widget, propertyName);
            this.widget = widget;
        }
    }
}