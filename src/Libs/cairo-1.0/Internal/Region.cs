using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Region
{
    // Not currently wrapped:
    // - cairo_region_create_rectangles (needs to be an array of RectangleIntData?)
    // - cairo_region_equal (if wrapped, need a hash function to go along with it)

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_contains_point")]
    public static extern bool ContainsPoint(RegionHandle handle, int x, int y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_contains_rectangle")]
    public static extern RegionOverlap ContainsRectangle(RegionHandle handle, RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_copy")]
    public static extern RegionOwnedHandle Copy(RegionHandle original);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_create")]
    public static extern RegionOwnedHandle Create();

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_create_rectangle")]
    public static extern RegionOwnedHandle CreateRectangle(RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_get_extents")]
    public static extern void GetExtents(RegionHandle handle, RectangleIntHandle extents);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_get_rectangle")]
    public static extern void GetRectangle(RegionHandle handle, int nth, RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_intersect")]
    public static extern Status Intersect(RegionHandle handle, RegionHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_intersect_rectangle")]
    public static extern Status IntersectRectangle(RegionHandle handle, RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_is_empty")]
    public static extern bool IsEmpty(RegionHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_num_rectangles")]
    public static extern int NumRectangles(RegionHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_status")]
    public static extern Status Status(RegionHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_subtract")]
    public static extern Status Subtract(RegionHandle handle, RegionHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_subtract_rectangle")]
    public static extern Status SubtractRectangle(RegionHandle handle, RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_translate")]
    public static extern void Translate(RegionHandle handle, int x, int y);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_union")]
    public static extern Status Union(RegionHandle handle, RegionHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_union_rectangle")]
    public static extern Status UnionRectangle(RegionHandle handle, RectangleIntHandle rectangle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_xor")]
    public static extern Status Xor(RegionHandle handle, RegionHandle other);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_region_xor_rectangle")]
    public static extern Status XorRectangle(RegionHandle handle, RectangleIntHandle rectangle);
}
