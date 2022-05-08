using System.Runtime.CompilerServices;

namespace GdkPixbuf;

internal partial class Module
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        Internal.ImportResolver.RegisterAsDllImportResolver();
        RegisterTypes();
    }

    static partial void RegisterTypes();
}
