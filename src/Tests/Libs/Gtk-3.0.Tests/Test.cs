using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

public abstract class Test
{
    [TestCleanup]
    public void Cleanup()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}
