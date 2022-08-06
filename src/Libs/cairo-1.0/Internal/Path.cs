using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Path
    {
        [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_path_destroy")]
        public static extern void Destroy(IntPtr handle);
    }
}
