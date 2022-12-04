using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Surface
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_create_similar")]
    public static extern SurfaceOwnedHandle CreateSimilar(SurfaceHandle handle, Content content, int width, int height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_create_similar_image")]
    public static extern SurfaceOwnedHandle CreateSimilarImage(SurfaceHandle handle, Format format, int width, int height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_create_for_rectangle")]
    public static extern SurfaceOwnedHandle CreateForRectangle(SurfaceHandle handle, double x, double y, double width, double height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_content")]
    public static extern Content GetContent(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_device")]
    public static extern DeviceUnownedHandle GetDevice(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_device_offset")]
    public static extern void GetDeviceOffset(SurfaceHandle handle, out double xOffset, out double yOffset);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_device_scale")]
    public static extern void GetDeviceScale(SurfaceHandle handle, out double xScale, out double yScale);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_fallback_resolution")]
    public static extern void GetFallbackResolution(SurfaceHandle handle, out double xPixelsPerInch, out double yPixelsPerInch);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_font_options")]
    public static extern void GetFontOptions(SurfaceHandle handle, FontOptionsHandle options);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_get_type")]
    public static extern SurfaceType GetType(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_finish")]
    public static extern void Finish(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_flush")]
    public static extern void Flush(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_mark_dirty")]
    public static extern void MarkDirty(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_mark_dirty_rectangle")]
    public static extern void MarkDirtyRectangle(SurfaceHandle handle, int x, int y, int width, int height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_set_device_offset")]
    public static extern void SetDeviceOffset(SurfaceHandle handle, double xOffset, double yOffset);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_set_device_scale")]
    public static extern void SetDeviceScale(SurfaceHandle handle, double xScale, double yScale);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_set_fallback_resolution")]
    public static extern void SetFallbackResolution(SurfaceHandle handle, double xPixelsPerInch, double yPixelsPerInch);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_surface_status")]
    public static extern Status Status(SurfaceHandle handle);
}
