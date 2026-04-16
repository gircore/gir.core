using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace Gtk;

public static class Module
{
    private static bool IsInitialized;
    private static DllImportResolver? CustomDllImportResolver;

    /// <summary>
    /// Initialize the <c>Gtk</c> module.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Calling this method is necessary to correctly initialize the bindings
    /// and should be done before using anything else in the <see cref="Gtk" />
    /// namespace.
    /// </para>
    /// <para>
    /// Calling this method will also initialize the modules this module
    /// depends on:
    /// </para>
    /// <list type="table">
    /// <item><description><see cref="Gdk.Module" /></description></item>
    /// <item><description><see cref="Gsk.Module" /></description></item>
    /// </list>
    /// <para>
    /// If the <see cref="Gtk.Application" /> class is used there is no need to
    /// call this method, because this module will be implicitly initialized
    /// when accessing <see cref="Gtk.Application" /> the first time.
    /// </para>
    /// </remarks>
    public static void Initialize()
    {
        if (IsInitialized)
            return;

        // Set immediately as initialized as static constructors like from Gio.Application 
        // which get called during "TypeRegistration.RegisterTypes" will call this method again
        // resulting in a double execution. A second try would probably make no difference.
        IsInitialized = true;

        Gdk.Module.Initialize();
        Gsk.Module.Initialize();

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, CustomDllImportResolver ?? Internal.ImportResolver.Resolve);
        Internal.TypeRegistration.RegisterTypes();
        Internal.Functions.Init();

        IsInitialized = true;
    }

    /// <summary>
    /// Set a custom DllImportResolver. This disables the automatic loading of native binaries for
    /// Gtk. If the given DllImportResolver receives the library name "Gtk" it has to return a pointer
    /// to the desired native Gtk binary.
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
