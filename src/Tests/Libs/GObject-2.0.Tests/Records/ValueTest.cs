using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class ValueTest
    {
        [DataTestMethod]
        [DataRow(5)]
        [DataRow(true)]
        [DataRow("TestString")]
        public void ValueFromDataShouldContainGivenData(object data)
        {
            var v = Value.From(data);
            v.Extract().Should().Be(data);
        }

        [TestMethod]
        public void ValueReturnsExpectedType()
        {
            // Check that we have a value
            var v = Value.From("Hello");
            Internal.Functions.TypeCheckValue(v.Handle).Should().Be(true);

            // Check we can marshal as a struct
            Internal.Value.Struct str = Marshal.PtrToStructure<Internal.Value.Struct>(v.Handle.DangerousGetHandle());
            Internal.Functions.TypeCheckValueHolds(v.Handle, str.GType).Should().BeTrue();
            Internal.Functions.TypeCheckValueHolds(v.Handle, (nuint) Internal.BasicType.String).Should().BeTrue();
            str.GType.Should().Be((nuint) Internal.BasicType.String);
        }
    }
}
