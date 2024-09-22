using System.Runtime.InteropServices;

namespace GObject.Internal;

public partial class Functions
{
    // "g_strv_get_type" method is defined in GLib gir file but is
    // historically part of the GObject shared library. This is
    // why it is defined manually here.
    [DllImport(ImportResolver.Library, EntryPoint = "g_strv_get_type")]
    internal static extern nuint StrvGetType();

    /// <summary>
    /// Returns whether the given type is a fundamental type.
    /// </summary>
    /// <returns>True if the type is fundamental otherwise false.</returns>
    public static bool IsFundamental(nuint type)
    {
        //255 << 2 corresponds to G_TYPE_FUNDAMENTAL_MAX
        return type <= (255 << 2);
    }
}
