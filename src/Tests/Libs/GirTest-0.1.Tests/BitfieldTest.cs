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
}
