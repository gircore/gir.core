using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Cairo.Internal;

namespace Cairo;

internal partial class Module
{
    private static void SetDllImportResolver()
    {
        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, Resolve);
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
            CairoImportResolver.Library => CairoImportResolver.Resolve(libraryName, assembly, searchPath),
            ImportResolver.Library => ImportResolver.Resolve(libraryName, assembly, searchPath),
            _ => IntPtr.Zero
        };
    }
}
