using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cairo.Tests;

[TestClass, TestCategory("UnitTest")]
public class FontOptionsTest : Test
{
    [TestMethod]
    public void BindingsShouldSucceed()
    {
        var opts = new FontOptions();
        opts.Status.Should().Be(Status.Success);

        opts.Antialias = Antialias.Gray;
        opts.Antialias.Should().Be(Antialias.Gray);

        opts.HintMetrics = HintMetrics.On;
        opts.HintMetrics.Should().Be(HintMetrics.On);

        opts.HintStyle = HintStyle.Slight;
        opts.HintStyle.Should().Be(HintStyle.Slight);

        opts.SubpixelOrder = SubpixelOrder.Vrgb;
        opts.SubpixelOrder.Should().Be(SubpixelOrder.Vrgb);

        opts.Variations = null;
        opts.Variations.Should().BeNull();
        opts.Variations = "foo";
        opts.Variations.Should().Be("foo");

        FontOptions copy = opts.Copy();
        copy.Status.Should().Be(Status.Success);
        copy.Should().Be(opts);
        copy.GetHashCode().Should().Be(opts.GetHashCode());

        copy.Variations = "bar";
        copy.Should().NotBe(opts);
        copy.GetHashCode().Should().NotBe(opts.GetHashCode());

        var merge = new FontOptions();
        merge.Antialias = Antialias.Subpixel;
        opts.Merge(merge);
        opts.Antialias.Should().Be(Antialias.Subpixel);
        opts.Variations.Should().Be("foo");
    }
}
