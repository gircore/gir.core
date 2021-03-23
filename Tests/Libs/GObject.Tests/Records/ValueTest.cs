using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests.Records
{
    [TestClass]
    public class ValueTest
    {
        [DataTestMethod]
        [DataRow(5)]
        public void ValueFromShouldContainGivenData(object data)
        {
            var v = Value.From(data);
            v.Extract().Should().Be(data);
        }
    }
}
