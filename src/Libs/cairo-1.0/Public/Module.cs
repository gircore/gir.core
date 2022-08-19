using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Cairo;

public class Module
{
    private static bool IsInitialized;

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
