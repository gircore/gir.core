using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class PatternTest : Test
    {
        [TestMethod]
        public void BindingsShouldSucceed()
        {
            var solid_pattern = SolidPattern.CreateRgb(0.2, 0.3, 0.4);
            solid_pattern.Status.Should().Be(Status.Success);

            solid_pattern.Extend = Extend.Reflect;
            solid_pattern.Extend.Should().Be(Extend.Reflect);

            solid_pattern.Filter = Filter.Gaussian;
            solid_pattern.Filter.Should().Be(Filter.Gaussian);

            var matrix = new Matrix(Internal.MatrixManagedHandle.Create());
            solid_pattern.GetMatrix(matrix);
            solid_pattern.SetMatrix(matrix);

            solid_pattern = SolidPattern.CreateRgba(0.2, 0.3, 0.4, 0.5);
            solid_pattern.Status.Should().Be(Status.Success);
            solid_pattern.PatternType.Should().Be(PatternType.Solid);

            var status = solid_pattern.GetRgba(out double r, out double g, out double b, out double a);
            status.Should().Be(Status.Success);
            a.Should().Be(0.5);

            var surf_pattern = new SurfacePattern(new ImageSurface(Format.Argb32, 32, 32));
            surf_pattern.Status.Should().Be(Status.Success);

            surf_pattern.PatternType.Should().Be(PatternType.Surface);
            surf_pattern.GetSurface().Status.Should().Be(Status.Success);

            var linear_pattern = new LinearGradient(1, 2, 3, 4);
            linear_pattern.Status.Should().Be(Status.Success);

            status = linear_pattern.GetLinearPoints(out double x0, out double y0, out double x1, out double y1);
            status.Should().Be(Status.Success);
            y1.Should().Be(4);

            linear_pattern.AddColorStopRgba(0.1, 0.2, 0.3, 0.4, 0.5);
            linear_pattern.ColorStopCount.Should().Be(1);
            status = linear_pattern.GetColorStopRgba(0, out double offset, out r, out g, out b, out a);
            status.Should().Be(Status.Success);
            a.Should().Be(0.5);

            var radial_pattern = new RadialGradient(1, 2, 3, 4, 5, 6);
            radial_pattern.Status.Should().Be(Status.Success);
        }
    }
}
