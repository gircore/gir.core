using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

[TestClass, TestCategory("SystemTest")]
public class InterfaceTest : Test
{
    [TestMethod]
    public void CanSetInterfaceProperty()
    {
        var entry = new Entry();
        entry.Editable.Should().BeTrue();
        entry.Editable = false;
        entry.Editable.Should().BeFalse();
        entry.Editable = true;
        entry.Editable.Should().BeTrue();
    }

    [TestMethod]
    public void CanCallInterfaceMethod()
    {
        const string Text = "test";
        var entry = new Entry();
        entry.SetText(Text);
        entry.GetText().Should().Be(Text);
    }
}
