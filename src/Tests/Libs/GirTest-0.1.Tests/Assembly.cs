using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

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

        // Set the filename encoding to something other than UTF-8 to detect
        // issues with marshalling 'filename' parameter types.
        GLib.Functions.Setenv("G_FILENAME_ENCODING", "ISO-8859-1", true);
    }
}
