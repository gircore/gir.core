using System.Runtime.InteropServices;

namespace GirTest;

public static class Module
{
    private static bool IsInitialized;

    public static void Initialize()
    {
        if (IsInitialized)
            return;

        GObject.Module.Initialize();

        NativeLibrary.SetDllImportResolver(typeof(Module).Assembly, Internal.ImportResolver.Resolve);
        Internal.TypeRegistration.RegisterTypes();

        IsInitialized = true;
    }
}
