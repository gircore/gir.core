using System.Runtime.CompilerServices;

namespace DiagnosticAnalyzerTestProject;

internal class Module
{
#pragma warning disable CA2255
    [ModuleInitializer]
    internal static void M1()
    {
        Gtk.Module.Initialize();
    }
#pragma warning restore CA2255
}
