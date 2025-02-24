using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

[TestClass, TestCategory("SystemTest")]
public class MethodTest
{
    [TestMethod]
    public void CanReturnNullIfReturnTypeIsInterface()
    {
        var fc = FileChooserNative.New("test", null, FileChooserAction.SelectFolder, "OK", "Cancel");
        fc.GetFile().Should().BeNull();
    }

    [TestMethod]
    public void CanReturnNullIfReturnTypeIsClass()
    {
        var l = Label.New("test");
        l.GetExtraMenu().Should().BeNull();
    }
}
