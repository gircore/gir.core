using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class FontOptions
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_copy")]
        public static extern FontOptionsOwnedHandle Copy(FontOptionsHandle original);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_create")]
        public static extern FontOptionsOwnedHandle Create();

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_destroy")]
        public static extern void Destroy(FontOptionsOwnedHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_equal")]
        public static extern bool Equal(FontOptionsHandle handle, FontOptionsHandle other);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_get_antialias")]
        public static extern Antialias GetAntialias(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_get_hint_metrics")]
        public static extern HintMetrics GetHintMetrics(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_get_hint_style")]
        public static extern HintStyle GetHintStyle(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_get_subpixel_order")]
        public static extern SubpixelOrder GetSubpixelOrder(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_get_variations")]
        public static extern IntPtr GetVariations(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_hash")]
        public static extern ulong Hash(FontOptionsHandle handle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_merge")]
        public static extern void Merge(FontOptionsHandle handle, FontOptionsHandle other);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_set_antialias")]
        public static extern void SetAntialias(FontOptionsHandle handle, Antialias antialias);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_set_hint_metrics")]
        public static extern void SetHintMetrics(FontOptionsHandle handle, HintMetrics hintMetrics);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_set_hint_style")]
        public static extern void SetHintStyle(FontOptionsHandle handle, HintStyle hintStyle);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_set_subpixel_order")]
        public static extern void SetSubpixelOrder(FontOptionsHandle handle, SubpixelOrder subpixelOrder);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_set_variations")]
        public static extern void SetVariations(FontOptionsHandle handle, [MarshalAs(UnmanagedType.LPUTF8Str)] string variations);

        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_status")]
        public static extern Status Status(FontOptionsHandle handle);
    }
}
