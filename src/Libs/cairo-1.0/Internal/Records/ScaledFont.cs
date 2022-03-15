using System;
using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class ScaledFont
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_scaled_font_destroy")]
        public static extern void Destroy(ScaledFontOwnedHandle handle);
    }
}
