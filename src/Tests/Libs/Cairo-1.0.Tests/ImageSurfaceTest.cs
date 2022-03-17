using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class ImageSurfaceTest
    {
        [TestMethod]
        public void ImageSurfaceConstructorShouldSucceed()
        {
            var surf = new Cairo.ImageSurface(Cairo.Format.Argb32, 800, 600);
            surf.Width.Should().Be(800);
            surf.Height.Should().Be(600);
            surf.Format.Should().Be(Cairo.Format.Argb32);
            surf.Stride.Should().Be(3200);
        }
    }
}
