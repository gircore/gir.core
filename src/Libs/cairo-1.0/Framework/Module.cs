using System.Runtime.CompilerServices;

namespace Cairo;

internal partial class Module
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        SetDllImportResolver();
        RegisterTypes();
    }

    static partial void RegisterTypes();
}
