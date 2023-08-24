using FluentAssertions;
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
}
