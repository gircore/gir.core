using System;
using System.Runtime.InteropServices;

/*
 * Important: This are functions which are part of GObject but made available manually in GLib
 */

namespace GLib.Internal;

public partial class Functions
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_boxed_copy")]
    public static extern IntPtr BoxedCopy(nuint boxedType, IntPtr srcBoxed);

    [DllImport(ImportResolver.Library, EntryPoint = "g_boxed_free")]
    public static extern void BoxedFree(nuint boxedType, IntPtr boxed);
}
