using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests;

[TestClass, TestCategory("UnitTest")]
public class FontFaceTest : Test
{
    [TestMethod]
    public void ImplementsIDisposable()
    {
        typeof(FontFace).Should().Implement<IDisposable>();
    }

    [TestMethod]
    public void BindingsShouldSucceed()
    {
        var face = new ToyFontFace("serif", FontSlant.Italic, FontWeight.Bold);
        face.Status.Should().Be(Status.Success);
        face.FontType.Should().Be(FontType.Toy);
        face.Family.Should().Be("serif");
        face.Slant.Should().Be(FontSlant.Italic);
        face.Weight.Should().Be(FontWeight.Bold);
    }
}
