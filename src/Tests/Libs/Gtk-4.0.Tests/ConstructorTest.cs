using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

[TestClass, TestCategory("SystemTest")]
public class ConstructorTest : Test
{
    [TestMethod]
    public void WindowConstructorShouldSetTitle()
    {
        var title = "MyTitle";
        var label = Label.New(title);

        label.GetLabel().Should().Be(title);
    }

    [TestMethod]
    public void CreateLabelWithNullTextShouldNotThrow()
    {
        System.Action createLabelWithNullText = () => Label.New(null);
        createLabelWithNullText.Should().NotThrow();
    }
}
