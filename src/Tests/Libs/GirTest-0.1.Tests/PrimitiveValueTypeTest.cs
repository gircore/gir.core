using System;
using FluentAssertions;
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
    public void InOutParameterShouldSucceed()
    {
        int val = 5;
        GirTest.PrimitiveValueTypeTester.IntInOut(ref val);
        val.Should().Be(10);
    }

    [DataTestMethod]
    [DataRow(0, -1)] //TODO: Make this work, there is something big missing here
    [DataRow(10, 20)]
    public void NullableInOutParameterShouldSucceed(int value, int result)
    {
        GirTest.PrimitiveValueTypeTester.IntInOutNullable(ref value);
        value.Should().Be(result);
    }

    [TestMethod]
    public void OutParameterShouldSucceed()
    {
        GirTest.PrimitiveValueTypeTester.IntOut(out int result);
        result.Should().Be(42);
    }
    
    [DataTestMethod]
    [DataRow(true, 0)]
    [DataRow(false, 42)]
    public void NullableOutParameterShouldSucceed(bool returnNull, int ret)
    {
        GirTest.PrimitiveValueTypeTester.IntOutNullable(returnNull, out int result);
        result.Should().Be(ret);
    }
}
