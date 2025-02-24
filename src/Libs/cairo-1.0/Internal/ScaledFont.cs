using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class ScaledFont
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_create")]
    public static extern ScaledFontOwnedHandle Create(FontFaceHandle font_face, MatrixHandle font_matrix, MatrixHandle ctm, FontOptionsHandle options);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_extents")]
    public static extern void Extents(ScaledFontHandle handle, out FontExtents extents);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_ctm")]
    public static extern void GetCtm(ScaledFontHandle handle, MatrixHandle ctm);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_font_face")]
    public static extern FontFaceUnownedHandle GetFontFace(ScaledFontHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_font_matrix")]
    public static extern void GetFontMatrix(ScaledFontHandle handle, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_font_options")]
    public static extern void GetFontOptions(ScaledFontHandle handle, FontOptionsHandle options);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_scale_matrix")]
    public static extern void GetScaleMatrix(ScaledFontHandle handle, MatrixHandle matrix);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_get_type")]
    public static extern FontType GetType(ScaledFontHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_status")]
    public static extern Status Status(ScaledFontHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_text_extents")]
    public static extern void TextExtents(ScaledFontHandle handle, GLib.Internal.NonNullableUtf8StringHandle utf8, out TextExtents extents);
}
