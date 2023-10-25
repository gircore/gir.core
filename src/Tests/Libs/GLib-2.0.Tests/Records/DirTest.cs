using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class DirTest
{
    [TestMethod]
    public void CanDispose()
    {
        //TODO: Enable once Dir annotations are fixed
        //See: https://gitlab.gnome.org/GNOME/glib/-/merge_requests/3566
        Assert.Inconclusive();

        var dir = (IDisposable) Dir.Open(".", 0);
        dir.Dispose();
    }
}
