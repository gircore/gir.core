using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class PropertyTest
{
    [TestMethod]
    public void TestIntegerProperty()
    {
        var pixbuf = Pixbuf.NewFromFile("test.bmp");
        pixbuf.Width.Should().Be(500);
        pixbuf.Height.Should().Be(500);
        pixbuf.NChannels.Should().Be(3);
    }

    [TestMethod]
    public void TestBoolProperty()
    {
        var pixbuf = Pixbuf.NewFromFile("test.bmp");
        pixbuf.HasAlpha.Should().Be(false);
    }
}
