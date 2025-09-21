using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class ImageSurface
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_create")]
    public static extern SurfaceOwnedHandle Create(Cairo.Format format, int width, int height);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_create_for_data")]
    public static extern SurfaceOwnedHandle CreateForData(IntPtr data, Cairo.Format format, int width, int height, int stride);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_data")]
    public static extern IntPtr GetData(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_format")]
    public static extern Cairo.Format GetFormat(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_height")]
    public static extern int GetHeight(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_stride")]
    public static extern int GetStride(SurfaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_image_surface_get_width")]
    public static extern int GetWidth(SurfaceHandle handle);
}
