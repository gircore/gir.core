using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Surface
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_surface_destroy")]
        public static extern void Destroy(SurfaceOwnedHandle handle);
    }
}
