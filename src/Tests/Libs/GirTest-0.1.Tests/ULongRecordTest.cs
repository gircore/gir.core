using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

/// <summary>
/// This tests correspond to GLib uint64 in the C code.
/// The C type is an usigned 64 bit integer which maps to C# ulong.
/// </summary>
[TestClass, TestCategory("BindingTest")]
public class ULongRecordTest : Test
{
    [TestMethod]
    public void ShouldUseLongInInternalStructData()
    {
        var data = new GirTest.Internal.ULongRecordTesterData();
        data.L.Should().BeOfType(typeof(ulong));
    }

    [TestMethod]
    public void GetSizeOfLShouldBeSizeOfLong()
    {
        var obj = new ULongRecordTester();
        obj.GetSizeofL().Should().Be(sizeof(ulong));
    }

    [TestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void ShouldHandleMaxMinLongValue(ulong value)
    {
        var obj = new ULongRecordTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    public void ReturnsMaxLongValue()
    {
        ULongRecordTester.GetMaxUlongValue().Should().Be(ulong.MaxValue);
    }

    [TestMethod]
    public void SetsMaxLongValue()
    {
        ULongRecordTester.IsMaxUlongValue(ulong.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void CanPassLongValueThroughCallback(ulong value)
    {
        ulong Callback(ulong val)
        {
            return val;
        }

        ULongRecordTester.RunCallback(value, Callback).Should().Be(value);
    }
}
