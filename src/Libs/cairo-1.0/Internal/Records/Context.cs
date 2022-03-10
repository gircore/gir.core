using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class Context
    {
        private const string CairoLib = "cairo-graphics";

        [DllImport(CairoLib, EntryPoint = "cairo_fill")]
        public static extern void Fill(ContextHandle cr);

        [DllImport(CairoLib, EntryPoint = "cairo_font_extents")]
        public static extern void FontExtents(ContextHandle cr, out FontExtents extents);

        [DllImport(CairoLib, EntryPoint = "cairo_move_to")]
        public static extern void MoveTo(ContextHandle cr, double x, double y);

        [DllImport(CairoLib, EntryPoint = "cairo_rectangle")]
        public static extern void Rectangle(ContextHandle cr, double x, double y, double width, double height);

        [DllImport(CairoLib, EntryPoint = "cairo_set_font_size")]
        public static extern void SetFontSize(ContextHandle cr, double size);

        [DllImport(CairoLib, EntryPoint = "cairo_set_source_rgba")]
        public static extern void SetSourceRgba(ContextHandle cr, double red, double green, double blue, double alpha);

        [DllImport(CairoLib, EntryPoint = "cairo_show_text")]
        public static extern void ShowText(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8);

        [DllImport(CairoLib, EntryPoint = "cairo_text_extents")]
        public static extern void TextExtents(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8, out TextExtents extents);
    }
}
