using System;
using System.Runtime.InteropServices;

namespace Cairo.Internal;

public partial class Device
{
    [DllImport(CairoImportResolver.Library, EntryPoint = "cairo_device_destroy")]
    public static extern void Destroy(IntPtr handle);
}
