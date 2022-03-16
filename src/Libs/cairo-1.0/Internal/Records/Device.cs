using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal
{
    public partial class Device
    {
        [DllImport(DllImportOverride.CairoLib, EntryPoint = "cairo_device_destroy")]
        public static extern void Destroy(DeviceOwnedHandle handle);
    }
}
