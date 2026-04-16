using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace GLib;

public static class Module
{
    private static bool IsInitialized;
    private static DllImportResolver? CustomDllImportResolver;

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

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, CustomDllImportResolver ?? Resolve);

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

    /// <summary>
    /// Set a custom DllImportResolver. This disables the automatic loading of native binaries for
    /// GLib. If the given DllImportResolver receives the library name "GLib" or "GObject" it has to return a pointer
    /// to the desired native GLib/GObject binary (e.g. libglib-2.0.so.0 or libgobject-2.0.so.0).
    /// </summary>
    /// <remarks>
    /// Please be aware that using this API means you are out of the officially supported area
    /// as you are able to combine GirCore with some binary the package was not build for. Please consider
    /// to generate a custom GirCore package which exactly matches your binary.
    /// </remarks>
    /// <param name="customDllImportResolver">Custom DllImportResolver to use.</param>
    /// <exception cref="Exception">Throws an exception if the method is called after module initialization.</exception>
    [Experimental("GirCore1009", UrlFormat = "https://gircore.github.io/docs/integration/diagnostic/1009.html")]
    public static void SetCustomDllImportResolver(DllImportResolver customDllImportResolver)
    {
        if (IsInitialized)
            throw new Exception("Can't set a custom DllImportResolver after initialization is done.");

        CustomDllImportResolver = customDllImportResolver;
    }
}
