using System.Runtime.CompilerServices;
using GLib.Internal;

namespace GLib;

internal class Module
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        ImportResolver.RegisterAsDllImportResolver();
    }
}
