using System;
using GObject;

namespace Gtk
{
    public partial class Widget
    {
        public static readonly Property<bool> VExpandProperty = Property<bool>.Register<Widget>(
            Native.VexpandProperty,
            nameof(VExpand),
            (o) => o.VExpand,
            (o, v) => o.VExpand = v
        );

        public bool VExpand
        {
            get => GetProperty(VExpandProperty);
            set => SetProperty(VExpandProperty, value);
        }

        public static readonly Property<bool> HExpandProperty = Property<bool>.Register<Widget>(
            Native.HexpandProperty,
            nameof(HExpand),
            (o) => o.HExpand,
            (o, v) => o.HExpand = v
        );

        public bool HExpand
        {
            get => GetProperty(HExpandProperty);
            set => SetProperty(HExpandProperty, value);
        }


        public static readonly Property<Align> VAlignProperty = Property<Align>.Register<Widget>(
            Native.ValignProperty,
            nameof(VAlign),
            (o) => o.VAlign,
            (o, v) => o.VAlign = v
        );

        public Align VAlign
        {
            get => GetProperty(VAlignProperty);
            set => SetProperty(VAlignProperty, value);
        }


        public static readonly Property<Align> HAlignProperty = Property<Align>.Register<Widget>(
            Native.HalignProperty,
            nameof(HAlign),
            (o) => o.HAlign,
            (o, v) => o.HAlign = v
        );

        public Align HAlign
        {
            get => GetProperty(HAlignProperty);
            set => SetProperty(HAlignProperty, value);
        }
        
        
        public static readonly Property<int> HeightRequestProperty = Property<int>.Register<Widget>(
            Native.HeightRequestProperty,
            nameof(HeightRequest),
            (o) => o.HeightRequest,
            (o, v) => o.HeightRequest = v
        );
    
        public int HeightRequest
        {
            get => GetProperty(HeightRequestProperty);
            set => SetProperty(HeightRequestProperty, value);
        }
        
        
        public static readonly Property<int> WidthRequestProperty = Property<int>.Register<Widget>(
            Native.WidthRequestProperty,
            nameof(WidthRequest),
            (o) => o.WidthRequest,
            (o, v) => o.WidthRequest = v
        );
    
        public int WidthRequest
        {
            get => GetProperty(WidthRequestProperty);
            set => SetProperty(WidthRequestProperty, value);
        }


        public void ShowAll() => Native.show_all(Handle);
        public void Show() => Native.show(Handle);
        public StyleContext GetStyleContext() => WrapHandle<StyleContext>(Native.get_style_context(Handle), false);

        public Gdk.Screen GetScreen() => WrapHandle<Gdk.Screen>(Native.get_screen(Handle), false);
    }
}
