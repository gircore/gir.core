using System;
using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class Path
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_path_destroy")]
        public static extern void Destroy(PathOwnedHandle handle);
    }
}
