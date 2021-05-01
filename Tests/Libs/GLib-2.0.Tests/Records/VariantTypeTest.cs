using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class VariantTypeTest
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
            var variantType = new VariantType(type);

            variantType.ToString().Should().Be(type);
        }

        [TestMethod]
        public void TypeStringIsString()
        {
            VariantType.String.ToString().Should().Be("s");
        }

        [TestMethod]
        public void TypeVariantIsVariant()
        {
            VariantType.Variant.ToString().Should().Be("v");
        }
    }
}
