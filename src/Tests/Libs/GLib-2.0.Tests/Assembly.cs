using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass]
public static class Assembly
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        Module.Initialize();
    }
}
