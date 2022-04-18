using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Context
    {
        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_destroy")]
        public static extern void Destroy(IntPtr handle);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_fill")]
        public static extern void Fill(ContextHandle cr);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_extents")]
        public static extern void FontExtents(ContextHandle cr, out FontExtents extents);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_move_to")]
        public static extern void MoveTo(ContextHandle cr, double x, double y);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rectangle")]
        public static extern void Rectangle(ContextHandle cr, double x, double y, double width, double height);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_font_size")]
        public static extern void SetFontSize(ContextHandle cr, double size);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_source_rgba")]
        public static extern void SetSourceRgba(ContextHandle cr, double red, double green, double blue, double alpha);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_show_text")]
        public static extern void ShowText(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_text_extents")]
        public static extern void TextExtents(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8, out TextExtents extents);
    }
}
