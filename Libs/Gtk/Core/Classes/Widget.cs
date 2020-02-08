using System;
using System.Reflection;
using GObject.Core;

namespace Gtk.Core
{
    public partial class GWidget : GObject.Core.GObject, Widget
    {
        private GBuilder? builder;

        public Property<int> WidthRequest { get; }
        public Property<int> HeightRequest { get; }

        internal GWidget(string template, string obj, Assembly assembly) : this(new GBuilder(template, assembly), obj) {}

        internal GWidget(GBuilder builder, string obj) : this(builder.GetObject(obj))
        {
            this.builder = builder;
            builder.Connect(this);
        }

        internal GWidget(IntPtr handle) : base(handle, true) 
        {
            WidthRequest = Property<int>("width-request",
                get : GetInt,
                set : Set
            );

            HeightRequest = Property<int>("height-request",
                get : GetInt,
                set : Set
            );
        }

        public void Show() => Gtk.Widget.show(this);
        public void ShowAll() => Gtk.Widget.show_all(this);
    }
}