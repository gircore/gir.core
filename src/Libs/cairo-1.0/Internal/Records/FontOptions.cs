using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class FontOptions
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_font_options_destroy")]
        public static extern void Destroy(FontOptionsOwnedHandle handle);
    }
}
