using System;
using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class FontOptions
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_destroy")]
        public static extern void Destroy(FontOptionsOwnedHandle handle);
    }
}
