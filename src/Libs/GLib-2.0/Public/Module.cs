using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GLib;

public class Module
{
    private static bool IsInitialized;

    /// <summary>
    /// Initialize the <c>GLib</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="GLib" />
    /// namespace.
    /// </para>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, Resolve);

        IsInitialized = true;
    }

    private static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        // There is need for some GObject API which is manually implemented as part of GLib. To be able
        // to access this API the import resolver from GObject is available, too.
        return libraryName switch
        {
            GLib.Internal.ImportResolver.Library => GLib.Internal.ImportResolver.Resolve(libraryName, assembly, searchPath),
            GObject.Internal.ImportResolver.Library => GObject.Internal.ImportResolver.Resolve(libraryName, assembly, searchPath),
            _ => IntPtr.Zero
        };
    }
}
