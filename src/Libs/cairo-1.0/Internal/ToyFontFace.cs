using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public class ToyFontFace
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_toy_font_face_create")]
    public static extern FontFaceOwnedHandle Create([MarshalAs(UnmanagedType.LPUTF8Str)] string family, FontSlant slant, FontWeight weight);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_toy_font_face_get_family")]
    public static extern IntPtr GetFamily(FontFaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_toy_font_face_get_slant")]
    public static extern FontSlant GetSlant(FontFaceHandle handle);

    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_toy_font_face_get_weight")]
    public static extern FontWeight GetWeight(FontFaceHandle handle);
}
