using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cairo;

public static class Module
{
    private static bool IsInitialized;

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

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, Resolve);
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }

    private static IntPtr Resolve(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
    {
        // Cairo is split into two libraries 'libcairo' and 'libcairo-gobject':
        // - 'libcairo' provides the cairo api
        // - 'libcairo-gobject' provides the gobject integration for 'libcairo'
        // To differentiate both libraries the DllImportAttribute of a function provides
        // different library names wich allows to switch to the correct implementation

        return libraryName switch
        {
            Internal.CairoImportResolver.Library => Internal.CairoImportResolver.Resolve(libraryName, assembly, searchPath),
            Internal.ImportResolver.Library => Internal.ImportResolver.Resolve(libraryName, assembly, searchPath),
            _ => IntPtr.Zero
        };
    }
}
