using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class DirTest
{
    [TestMethod]
    public void CanDispose()
    {
        var dir = (IDisposable) Dir.Open(".", 0);
        dir.Dispose();
    }
}
