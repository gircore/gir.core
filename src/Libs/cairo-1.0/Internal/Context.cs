using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Context
{
    // TODO
    // - Bindings for cairo_copy_clip_rectangle_list() (cairo_rectangle_list_t)

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_clip")]
    public static extern void Clip(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_clip_extents")]
    public static extern void ClipExtents(ContextHandle cr, out double x1, out double y1, out double x2, out double y2);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_clip_preserve")]
    public static extern void ClipPreserve(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_copy_page")]
    public static extern void CopyPage(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_create")]
    public static extern ContextOwnedHandle Create(SurfaceHandle target);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_fill")]
    public static extern void Fill(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_fill_preserve")]
    public static extern void FillPreserve(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_fill_extents")]
    public static extern void FillExtents(ContextHandle cr, out double x1, out double y1, out double x2, out double y2);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_antialias")]
    public static extern Antialias GetAntialias(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_dash_count")]
    public static extern int GetDashCount(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_dash")]
    public static extern void GetDash(ContextHandle cr, double[] dashes, out double offset);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_fill_rule")]
    public static extern FillRule GetFillRule(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_group_target")]
    public static extern SurfaceUnownedHandle GetGroupTarget(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_line_cap")]
    public static extern LineCap GetLineCap(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_line_join")]
    public static extern LineJoin GetLineJoin(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_line_width")]
    public static extern double GetLineWidth(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_miter_limit")]
    public static extern double GetMiterLimit(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_operator")]
    public static extern Operator GetOperator(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_tolerance")]
    public static extern double GetTolerance(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_source")]
    public static extern PatternUnownedHandle GetSource(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_get_target")]
    public static extern SurfaceUnownedHandle GetTarget(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_in_clip")]
    public static extern bool InClip(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_in_fill")]
    public static extern bool InFill(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_in_stroke")]
    public static extern bool InStroke(ContextHandle cr, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_mask")]
    public static extern void Mask(ContextHandle cr, PatternHandle pattern);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_mask_surface")]
    public static extern void MaskSurface(ContextHandle cr, SurfaceHandle surface, double surface_x, double surface_y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_paint")]
    public static extern void Paint(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_paint_with_alpha")]
    public static extern void PaintWithAlpha(ContextHandle cr, double alpha);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pop_group")]
    public static extern PatternOwnedHandle PopGroup(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pop_group_to_source")]
    public static extern void PopGroupToSource(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_push_group")]
    public static extern void PushGroup(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_push_group_with_content")]
    public static extern void PushGroupWithContent(ContextHandle cr, Content content);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_reset_clip")]
    public static extern void ResetClip(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_restore")]
    public static extern void Restore(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_save")]
    public static extern void Save(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_antialias")]
    public static extern void SetAntialias(ContextHandle cr, Antialias antialias);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_dash")]
    public static extern void SetDash(ContextHandle cr, double[] dashes, int num_dashes, double offset);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_fill_rule")]
    public static extern void SetFillRule(ContextHandle cr, FillRule fill_rule);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_line_cap")]
    public static extern void SetLineCap(ContextHandle cr, LineCap line_cap);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_line_join")]
    public static extern void SetLineJoin(ContextHandle cr, LineJoin line_join);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_line_width")]
    public static extern void SetLineWidth(ContextHandle cr, double width);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_miter_limit")]
    public static extern void SetMiterLimit(ContextHandle cr, double limit);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_operator")]
    public static extern void SetOperator(ContextHandle cr, Operator op);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_source")]
    public static extern void SetSource(ContextHandle cr, PatternHandle source);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_source_rgb")]
    public static extern void SetSourceRgb(ContextHandle cr, double red, double green, double blue);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_source_rgba")]
    public static extern void SetSourceRgba(ContextHandle cr, double red, double green, double blue, double alpha);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_source_surface")]
    public static extern void SetSourceSurface(ContextHandle cr, SurfaceHandle surface, double x, double y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_set_tolerance")]
    public static extern void SetTolerance(ContextHandle cr, double tolerance);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_show_page")]
    public static extern void ShowPage(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_status")]
    public static extern Status Status(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_stroke")]
    public static extern void Stroke(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_stroke_preserve")]
    public static extern void StrokePreserve(ContextHandle cr);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_stroke_extents")]
    public static extern void StrokeExtents(ContextHandle cr, out double x1, out double y1, out double x2, out double y2);
}
