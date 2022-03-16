using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Region
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_region_destroy")]
        public static extern void Destroy(RegionOwnedHandle handle);
    }
}
