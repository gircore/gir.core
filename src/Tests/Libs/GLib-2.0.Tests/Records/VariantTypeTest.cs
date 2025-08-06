using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class VariantTypeTest : Test
{
    [DataTestMethod]
    [DataRow("s")]
    [DataRow("b")]
    [DataRow("y")]
    [DataRow("n")]
    [DataRow("q")]
    [DataRow("i")]
    [DataRow("u")]
    [DataRow("x")]
    [DataRow("t")]
    [DataRow("h")]
    [DataRow("t")]
    [DataRow("v")]
    public void CanCreateTypeFromString(string type)
    {
        var variantType = VariantType.New(type);

        variantType.DupString().Should().Be(type);
    }

    [TestMethod]
    public void TypeStringIsString()
    {
        VariantType.String.DupString().Should().Be("s");
    }

    [TestMethod]
    public void TypeVariantIsVariant()
    {
        VariantType.Variant.DupString().Should().Be("v");
    }
}
