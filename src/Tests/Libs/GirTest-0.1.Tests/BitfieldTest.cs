using FluentAssertions;
using GObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class BitfieldTest : Test
{
    [TestMethod]
    public void SupportsPointedBitfields()
    {
        var flags = BitfieldTesterSimpleFlags.One | BitfieldTesterSimpleFlags.Two;
        BitfieldTester.ResetFlags(ref flags);

        flags.Should().Be(BitfieldTesterSimpleFlags.Zero);
    }

    [TestMethod]
    public void CanBeUsedInGValue()
    {
        var flags = BitfieldTesterSimpleFlags.One | BitfieldTesterSimpleFlags.Two;
        var value = new Value(Type.Flags);
        value.SetFlags(flags);

        var result1 = value.Extract<BitfieldTesterSimpleFlags>();
        result1.Should().Be(flags);

        var result2 = value.GetFlags<BitfieldTesterSimpleFlags>();
        result2.Should().Be(flags);

        value.GetFlags().Should().Be((uint) flags);
    }

    [TestMethod]
    public void CanUseMaxInGValue()
    {
        var max = (uint) BitfieldTesterSimpleFlags.Max;
        max.Should().Be(1u << 31);

        var value = new Value(Type.Flags);
        value.Set(BitfieldTesterSimpleFlags.Max);

        value.Extract<BitfieldTesterSimpleFlags>().Should().Be(BitfieldTesterSimpleFlags.Max);
        value.GetFlags().Should().Be(1u << 31);
    }
}
