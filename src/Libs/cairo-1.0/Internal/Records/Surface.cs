using System;
using System.Runtime.InteropServices;

namespace cairo.Internal
{
    public partial class Surface
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_surface_destroy")]
        public static extern void Destroy(SurfaceOwnedHandle handle);
    }
}
