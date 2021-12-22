using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GdkPixbuf.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class MethodTest
    {
        [TestMethod]
        public void GetMethodTest()
        {
            var pixbuf = Pixbuf.NewFromFile("test.bmp");
            pixbuf.GetWidth().Should().Be(500);
            pixbuf.GetHeight().Should().Be(500);
            pixbuf.GetHasAlpha().Should().Be(false);
            pixbuf.GetNChannels().Should().Be(3);
        }
    }
}
