using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class VariantTest
    {
        [TestMethod]
        public void CanCreateInt()
        {
            int value = 5;
            var variant = Variant.Create(value);

            variant.GetInt().Should().Be(value);
        }
        
        [TestMethod]
        public void CanCreateString()
        {
            string value = "test";
            var variant = Variant.Create(value);

            variant.GetString().Should().Be(value);
        }
    }
}
