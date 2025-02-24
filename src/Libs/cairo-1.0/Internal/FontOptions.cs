using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class FontOptions
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_copy")]
    public static extern FontOptionsOwnedHandle Copy(FontOptionsHandle original);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_create")]
    public static extern FontOptionsOwnedHandle Create();

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_equal")]
    public static extern bool Equal(FontOptionsHandle handle, FontOptionsHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_get_antialias")]
    public static extern Antialias GetAntialias(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_get_hint_metrics")]
    public static extern HintMetrics GetHintMetrics(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_get_hint_style")]
    public static extern HintStyle GetHintStyle(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_get_subpixel_order")]
    public static extern SubpixelOrder GetSubpixelOrder(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_get_variations")]
    public static extern GLib.Internal.NullableUtf8StringUnownedHandle GetVariations(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_hash")]
    public static extern ulong Hash(FontOptionsHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_merge")]
    public static extern void Merge(FontOptionsHandle handle, FontOptionsHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_set_antialias")]
    public static extern void SetAntialias(FontOptionsHandle handle, Antialias antialias);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_set_hint_metrics")]
    public static extern void SetHintMetrics(FontOptionsHandle handle, HintMetrics hintMetrics);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_set_hint_style")]
    public static extern void SetHintStyle(FontOptionsHandle handle, HintStyle hintStyle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_set_subpixel_order")]
    public static extern void SetSubpixelOrder(FontOptionsHandle handle, SubpixelOrder subpixelOrder);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_set_variations")]
    public static extern void SetVariations(FontOptionsHandle handle, GLib.Internal.NullableUtf8StringHandle variations);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_options_status")]
    public static extern Status Status(FontOptionsHandle handle);
}
