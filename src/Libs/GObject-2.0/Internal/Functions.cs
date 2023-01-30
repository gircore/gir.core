using System.Runtime.InteropServices;

namespace GObject.Internal;

public partial class Functions
{
    // "g_strv_get_type" method is defined in GLib gir file but is
    // historically part of the GObject shared library. This is
    // why it is defined manually here.
    [DllImport("GObject", EntryPoint = "g_strv_get_type")]
    internal static extern nuint StrvGetType();
}
