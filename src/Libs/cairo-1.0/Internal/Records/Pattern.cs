using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Pattern
    {
        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_pattern_destroy")]
        public static extern void Destroy(IntPtr handle);
    }
}
