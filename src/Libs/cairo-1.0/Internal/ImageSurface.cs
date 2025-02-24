using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class ImageSurface
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_create")]
    public static extern SurfaceOwnedHandle Create(Format format, int width, int height);

    // Skip cairo_image_surface_create_for_data for now, as it has some
    // additional lifetime requirements for the input buffer.

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_data")]
    public static extern IntPtr GetData(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_format")]
    public static extern Format GetFormat(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_height")]
    public static extern int GetHeight(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_stride")]
    public static extern int GetStride(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_width")]
    public static extern int GetWidth(SurfaceHandle handle);
}
