using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests;

[TestClass, TestCategory("UnitTest")]
public class ImageSurfaceTest : Test
{
    [TestMethod]
    public void BindingsShouldSucceed()
    {
        var surf = new Cairo.ImageSurface(Cairo.Format.Argb32, 800, 600);
        surf.Status.Should().Be(Status.Success);

        // 4 bytes per pixel since the format is Argb32.
        Span<byte> data = surf.GetData();
        data.Length.Should().Be(surf.Width * surf.Height * 4);

        surf.Width.Should().Be(800);
        surf.Height.Should().Be(600);
        surf.Format.Should().Be(Cairo.Format.Argb32);
        surf.Stride.Should().Be(3200);

        surf.CreateSimilar(Content.Alpha, 400, 300).Status.Should().Be(Status.Success);
        surf.CreateSimilarImage(Format.Argb32, 400, 300).Status.Should().Be(Status.Success);
        surf.CreateForRectangle(0, 0, 400, 300).Status.Should().Be(Status.Success);

        var res = (4, 5);
        surf.FallbackResolution = res;
        surf.FallbackResolution.Should().Be(res);

        var offset = (2, 3);
        surf.DeviceOffset = offset;
        surf.DeviceOffset.Should().Be(offset);

        var scale = (1.2, 2.3);
        surf.DeviceScale = scale;
        surf.DeviceScale.Should().Be(scale);

        surf.Content.Should().Be(Content.ColorAlpha);
        surf.SurfaceType.Should().Be(SurfaceType.Image);

        var opts = new FontOptions();
        surf.GetFontOptions(opts);
        opts.Status.Should().Be(Status.Success);

        surf.Flush();
        surf.MarkDirty();
        surf.MarkDirty(1, 2, 3, 4);

        surf.Finish();
        surf.Status.Should().Be(Status.Success);
    }
}
