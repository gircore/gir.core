using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class LongTest : Test
{
    [TestMethod]
    public void ShouldUseLongInInternalStructData()
    {
        var data = new GirTest.Internal.LongTesterData();
        data.L.Should().BeOfType(typeof(long));
    }

    [TestMethod]
    public void GetSizeOfLShouldBeSizeOfLong()
    {
        var obj = new LongTester();
        obj.GetSizeofL().Should().Be(sizeof(long));
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void ShouldHandleMaxMinLongValue(long value)
    {
        var obj = new LongTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    public void ReturnsMaxLongValue()
    {
        LongTester.GetMaxLongValue().Should().Be(long.MaxValue);
    }

    [TestMethod]
    public void ReturnsMinLongValue()
    {
        LongTester.GetMinLongValue().Should().Be(long.MinValue);
    }

    [TestMethod]
    public void SetsMaxLongValue()
    {
        LongTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMinLongValue()
    {
        LongTester.IsMinLongValue(long.MinValue).Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void CanPassLongValueThroughCallback(long value)
    {
        long Callback(long val)
        {
            return val;
        }

        LongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
