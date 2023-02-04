using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests;

[TestClass]
public static class Assembly
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        Module.Initialize();
        GLib.Functions.LogSetAlwaysFatal(
            GLib.LogLevelFlags.LevelCritical
            | GLib.LogLevelFlags.LevelError
            | GLib.LogLevelFlags.LevelWarning
        );
    }
}
