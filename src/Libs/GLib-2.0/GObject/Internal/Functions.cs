using System;
using System.Runtime.InteropServices;

/*
 * Important: This are functions which are part of GObject but made available manually in GLib
 */

namespace GObject.Internal;

internal static class Functions
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_boxed_copy")]
    public static extern IntPtr BoxedCopy(nuint boxedType, IntPtr srcBoxed);

    [DllImport(ImportResolver.Library, EntryPoint = "g_boxed_free")]
    public static extern void BoxedFree(nuint boxedType, IntPtr boxed);

    [DllImport(ImportResolver.Library, EntryPoint = "g_type_name")]
    public static extern GLib.Internal.NullableUtf8StringUnownedHandle TypeName(GObject.Type type);

    // "g_strv_get_type" method is defined in GLib gir file but is
    // historically part of the GObject shared library. This is
    // why it is defined manually here.
    [DllImport(ImportResolver.Library, EntryPoint = "g_strv_get_type")]
    public static extern nuint StrvGetType();
}
