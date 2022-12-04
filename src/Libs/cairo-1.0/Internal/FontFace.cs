using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class FontFace
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_face_destroy")]
    public static extern void Destroy(IntPtr handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_face_get_type")]
    public static extern FontType GetType(FontFaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_font_face_status")]
    public static extern Status Status(FontFaceHandle handle);
}
