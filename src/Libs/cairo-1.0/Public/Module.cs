using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cairo;

public static class Module
{
    private static bool IsInitialized;
    private static DllImportResolver? CustomDllImportResolver;

    /// <summary>
    /// Initialize the <c>Cairo</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Cairo" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="GObject.Module" /></description></item>
    /// </list>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GObject.Module.Initialize();

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, CustomDllImportResolver ?? Resolve);
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }

    private static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        // Cairo is split into two libraries 'libcairo' and 'libcairo-gobject':
        // - 'libcairo' provides the cairo api
        // - 'libcairo-gobject' provides the gobject integration for 'libcairo'
        // To differentiate both libraries the DllImportAttribute of a function provides
        // different library names which allows to switch to the correct implementation

        return libraryName switch
        {
            Internal.CairoImportResolver.Library => Internal.CairoImportResolver.Resolve(libraryName, assembly, searchPath),
            Internal.ImportResolver.Library => Internal.ImportResolver.Resolve(libraryName, assembly, searchPath),
            _ => IntPtr.Zero
        };
    }

    /// <summary>
    /// Set a custom DllImportResolver. This disables the automatic loading of native binaries for
    /// cairo. If the given DllImportResolver receives the library name "cairo" or "cairo-graphics" it has to return a pointer
    /// to the desired native cairo binary (e.g. libcairo-gobject.so.2 or libcairo.so.2).
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
