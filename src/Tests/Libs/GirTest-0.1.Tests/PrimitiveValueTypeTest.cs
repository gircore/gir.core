using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PrimitiveValueTypeTest : Test
{
    [TestMethod]
    public void InParameterShouldSucceed()
    {
        GirTest.PrimitiveValueType.IntIn(5).Should().Be(10);
    }
}
