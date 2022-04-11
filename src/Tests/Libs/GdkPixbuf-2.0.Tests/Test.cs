using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests;

public abstract class Test
{
    [TestCleanup]
    public void Cleanup()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
