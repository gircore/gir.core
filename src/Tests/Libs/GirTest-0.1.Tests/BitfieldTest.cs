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
        value.Set(flags);
        var result = value.Extract<BitfieldTesterSimpleFlags>();
        result.Should().Be(flags);
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
