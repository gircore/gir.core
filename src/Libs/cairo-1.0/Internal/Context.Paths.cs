using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Context
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_append_path")]
    public static extern void AppendPath(ContextHandle cr, PathHandle path);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_arc")]
    public static extern void Arc(ContextHandle cr, double xc, double yc, double radius, double angle1, double angle2);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_arc_negative")]
    public static extern void ArcNegative(ContextHandle cr, double xc, double yc, double radius, double angle1, double angle2);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_close_path")]
    public static extern void ClosePath(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_copy_path")]
    public static extern PathOwnedHandle CopyPath(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_copy_path_flat")]
    public static extern PathOwnedHandle CopyPathFlat(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_curve_to")]
    public static extern void CurveTo(ContextHandle cr, double x1, double y1, double x2, double y2, double x3, double y3);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_current_point")]
    public static extern void GetCurrentPoint(ContextHandle cr, out double x, out double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_has_current_point")]
    public static extern bool HasCurrentPoint(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_line_to")]
    public static extern void LineTo(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_move_to")]
    public static extern void MoveTo(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_new_path")]
    public static extern void NewPath(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_new_sub_path")]
    public static extern void NewSubPath(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_path_extents")]
    public static extern void PathExtents(ContextHandle cr, out double x1, out double y1, out double x2, out double y2);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rectangle")]
    public static extern void Rectangle(ContextHandle cr, double x, double y, double width, double height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rel_curve_to")]
    public static extern void RelCurveTo(ContextHandle cr, double x1, double y1, double x2, double y2, double x3, double y3);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rel_line_to")]
    public static extern void RelLineTo(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_rel_move_to")]
    public static extern void RelMoveTo(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_text_path")]
    public static extern void TextPath(ContextHandle cr, [MarshalAs(UnmanagedType.LPUTF8Str)] string utf8);
}
