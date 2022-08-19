using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests;

[TestClass]
public static class Assembly
{
    [AssemblyInitialize]
    public static void Initialize(TestContext context)
    {
        Module.Initialize();
    }
}
