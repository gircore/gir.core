using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class ScaledFont
    {
        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_scaled_font_destroy")]
        public static extern void Destroy(IntPtr handle);
    }
}
