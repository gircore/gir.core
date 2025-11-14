using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

/// <summary>
/// This tests correspond to GLib int64 in the C code.
/// The C type is a signed 64 bit integer which maps to C# long.
/// </summary>
[TestClass, TestCategory("BindingTest")]
public class LongRecordTest : Test
{
    [TestMethod]
    public void ShouldUseLongInInternalStructData()
    {
        var data = new GirTest.Internal.LongRecordTesterData();
        data.L.Should().BeOfType(typeof(long));
    }

    [TestMethod]
    public void GetSizeOfLShouldBeSizeOfLong()
    {
        var obj = new LongRecordTester();
        obj.GetSizeofL().Should().Be(sizeof(long));
    }

    [TestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void ShouldHandleMaxMinLongValue(long value)
    {
        var obj = new LongRecordTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    public void ReturnsMaxLongValue()
    {
        LongRecordTester.GetMaxLongValue().Should().Be(long.MaxValue);
    }

    [TestMethod]
    public void ReturnsMinLongValue()
    {
        LongRecordTester.GetMinLongValue().Should().Be(long.MinValue);
    }

    [TestMethod]
    public void SetsMaxLongValue()
    {
        LongRecordTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMinLongValue()
    {
        LongRecordTester.IsMinLongValue(long.MinValue).Should().BeTrue();
    }

    [TestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void CanPassLongValueThroughCallback(long value)
    {
        long Callback(long val)
        {
            return val;
        }

        LongRecordTester.RunCallback(value, Callback).Should().Be(value);
    }
}
