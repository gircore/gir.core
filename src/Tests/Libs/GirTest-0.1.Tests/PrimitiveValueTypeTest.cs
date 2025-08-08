using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PrimitiveValueTypeTest : Test
{
    [TestMethod]
    public void InParameterShouldSucceed()
    {
        PrimitiveValueTypeTester.IntIn(5).Should().Be(10);
    }

    [TestMethod]
    public void PointedInParameterShouldSucceed()
    {
        int i = 21;
        PrimitiveValueTypeTester.IntPointerIn(ref i);

        i.Should().Be(42);
    }

    [TestMethod]
    public void InOutParameterShouldSucceed()
    {
        int val = 5;
        PrimitiveValueTypeTester.IntInOut(ref val);
        val.Should().Be(10);
        PrimitiveValueTypeTester.IntInOutOptional(ref val);
        val.Should().Be(20);
    }

    [TestMethod]
    public void OutParameterShouldSucceed()
    {
        PrimitiveValueTypeTester.IntOut(out int result);
        result.Should().Be(42);

        PrimitiveValueTypeTester.IntOutOptional(out int result2);
        result2.Should().Be(42);
    }
}
