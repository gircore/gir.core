using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Pattern
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_pattern_destroy")]
        public static extern void Destroy(PatternOwnedHandle handle);
    }
}
