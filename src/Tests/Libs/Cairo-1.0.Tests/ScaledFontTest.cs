using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests;

[TestClass, TestCategory("UnitTest")]
public class ScaledFontTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        typeof(ScaledFont).Should().Implement<IDisposable>();
    }

    [TestMethod]
    public void BindingsShouldSucceed()
    {
        var face = new ToyFontFace("serif", FontSlant.Italic, FontWeight.Bold);
        var matrix = new Matrix();
        var ctm = new Matrix();
        var font = new ScaledFont(face, matrix, ctm, new FontOptions());
        font.Status.Should().Be(Status.Success);
        font.FontType.Should().NotBe(FontType.Toy); // Should be the backend type, e.g. Quartz on macOS

        font.Extents(out FontExtents font_extents);
        font_extents.Ascent.Should().Be(0);

        font.TextExtents("foo", out TextExtents text_extents);
        text_extents.Height.Should().Be(0);

        font.GetFontMatrix(matrix);
        font.GetCtm(matrix);
        font.GetScaleMatrix(matrix);

        var options = new FontOptions();
        font.GetFontOptions(options);
        options.Status.Should().Be(Status.Success);

        font.GetFontFace().Status.Should().Be(Status.Success);
    }
}
