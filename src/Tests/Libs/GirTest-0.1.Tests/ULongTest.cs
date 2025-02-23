using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ULongTest : Test
{
    [TestMethod]
    public void ShouldUseLongInInternalStructData()
    {
        var data = new GirTest.Internal.ULongTesterData();
        data.L.Should().BeOfType(typeof(ulong));
    }

    [TestMethod]
    public void GetSizeOfLShouldBeSizeOfLong()
    {
        var obj = new ULongTester();
        obj.GetSizeofL().Should().Be(sizeof(ulong));
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void ShouldHandleMaxMinLongValue(ulong value)
    {
        var obj = new ULongTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    public void ReturnsMaxLongValue()
    {
        ULongTester.GetMaxUlongValue().Should().Be(ulong.MaxValue);
    }

    [TestMethod]
    public void SetsMaxLongValue()
    {
        ULongTester.IsMaxUlongValue(ulong.MaxValue).Should().BeTrue();
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void CanPassLongValueThroughCallback(ulong value)
    {
        ulong Callback(ulong val)
        {
            return val;
        }

        ULongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
