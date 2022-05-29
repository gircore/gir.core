using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Pattern
    {
        // TODO - expose cairo_pattern_create_mesh() and related methods.

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_add_color_stop_rgb")]
        public static extern void AddColorStopRgb(PatternHandle pattern, double offset, double r, double g, double b);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_add_color_stop_rgba")]
        public static extern void AddColorStopRgba(PatternHandle pattern, double offset, double r, double g, double b, double a);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_create_linear")]
        public static extern PatternOwnedHandle CreateLinear(double x0, double y0, double x1, double y1);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_create_radial")]
        public static extern PatternOwnedHandle CreateRadial(double cx0, double cy0, double radius0, double cx1, double cy1, double radius1);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_create_rgb")]
        public static extern PatternOwnedHandle CreateRgb(double r, double g, double b);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_create_rgba")]
        public static extern PatternOwnedHandle CreateRgba(double r, double g, double b, double a);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_create_for_surface")]
        public static extern PatternOwnedHandle CreateForSurface(SurfaceHandle surface);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_destroy")]
        public static extern void Destroy(IntPtr handle);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_color_stop_count")]
        public static extern Status GetColorStopCount(PatternHandle pattern, out int count);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_color_stop_rgba")]
        public static extern Status GetColorStopRgba(PatternHandle pattern, int index, out double offset, out double r, out double g, out double b, out double a);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_extend")]
        public static extern Extend GetExtend(PatternHandle pattern);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_filter")]
        public static extern Filter GetFilter(PatternHandle pattern);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_linear_points")]
        public static extern Status GetLinearPoints(PatternHandle pattern, out double x0, out double y0, out double x1, out double y1);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_matrix")]
        public static extern void GetMatrix(PatternHandle pattern, MatrixHandle matrix);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_rgba")]
        public static extern Status GetRgba(PatternHandle pattern, out double r, out double g, out double b, out double a);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_surface")]
        public static extern Status GetSurface(PatternHandle pattern, out SurfaceUnownedHandle surface);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_get_type")]
        public static extern PatternType GetType(PatternHandle pattern);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_set_extend")]
        public static extern void SetExtend(PatternHandle pattern, Extend extend);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_set_filter")]
        public static extern void SetFilter(PatternHandle pattern, Filter filter);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_set_matrix")]
        public static extern void SetMatrix(PatternHandle pattern, MatrixHandle matrix);

        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_status")]
        public static extern Status Status(PatternHandle pattern);
    }
}
