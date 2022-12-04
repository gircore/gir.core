using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Context
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_extents")]
    public static extern void FontExtents(ContextHandle cr, out FontExtents extents);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_font_face")]
    public static extern FontFaceUnownedHandle GetFontFace(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_font_matrix")]
    public static extern void GetFontMatrix(ContextHandle cr, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_font_options")]
    public static extern void GetFontOptions(ContextHandle cr, FontOptionsHandle options);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_scaled_font")]
    public static extern ScaledFontUnownedHandle GetScaledFont(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_select_font_face")]
    public static extern void SelectFontFace(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string family, FontSlant slant, FontWeight weight);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_font_face")]
    public static extern void SetFontFace(ContextHandle cr, FontFaceHandle font_face);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_font_matrix")]
    public static extern void SetFontMatrix(ContextHandle cr, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_font_options")]
    public static extern void SetFontOptions(ContextHandle cr, FontOptionsHandle options);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_font_size")]
    public static extern void SetFontSize(ContextHandle cr, double size);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_scaled_font")]
    public static extern void SetScaledFont(ContextHandle cr, ScaledFontHandle scaled_font);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_show_text")]
    public static extern void ShowText(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_text_extents")]
    public static extern void TextExtents(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8, out TextExtents extents);
}
