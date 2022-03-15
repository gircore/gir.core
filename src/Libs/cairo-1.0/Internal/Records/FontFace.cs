using System;
using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class FontFace
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_face_destroy")]
        public static extern void Destroy(FontFaceOwnedHandle handle);
    }
}
