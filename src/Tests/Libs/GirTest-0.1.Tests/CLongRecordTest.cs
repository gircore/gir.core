using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

/// <summary>
/// This tests correspond to GLib glong in the C code.
/// The C type is a signed 64/32 bit integer depending on the platform which maps to C# CLong.
/// </summary>
[TestClass, TestCategory("BindingTest")]
public class CLongRecordTest : Test
{
    [TestMethod]
    public void ShouldUseCLongInInternalStructData()
    {
        var data = new GirTest.Internal.CLongRecordTesterData();
        data.L.Should().BeOfType<CLong>();
    }

    [TestMethod]
    public unsafe void GetSizeOfLShouldBeSizeOfCLong()
    {
        var sizeOfCLong = sizeof(CLong);
        var obj = new CLongRecordTester();
        obj.GetSizeofL().Should().Be((nuint) sizeOfCLong);
    }

    [TestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void ShouldHandleMaxMinLongValue(long value)
    {
        var obj = new CLongRecordTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldHandleMaxMinIntValue(int value)
    {
        var obj = new CLongRecordTester { L = value };
        obj.L.Should().Be(value);
    }

    [TestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldThrowIfValueExceedsIntegerSize(long value)
    {
        var obj = new CLongRecordTester();
        var act = () => obj.L = value;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMaxLongValueOn64BitUnix()
    {
        CLongRecordTester.GetMaxLongValue().Should().Be(long.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMaxLongValueOnWindows()
    {
        CLongRecordTester.GetMaxLongValue().Should().Be(int.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMinLongValueOn64BitUnix()
    {
        CLongRecordTester.GetMinLongValue().Should().Be(long.MinValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMinLongValueOnWindows()
    {
        CLongRecordTester.GetMinLongValue().Should().Be(int.MinValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMaxLongValueOn64BitUnix()
    {
        CLongRecordTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMaxLongValueOnWindows()
    {
        CLongRecordTester.IsMaxLongValue(int.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMinLongValueOn64BitUnix()
    {
        CLongRecordTester.IsMinLongValue(long.MinValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMinLongValueOnWindows()
    {
        CLongRecordTester.IsMinLongValue(int.MinValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfLongMinValueIsPassedOnWindows()
    {
        var action = () => CLongRecordTester.IsMinLongValue(long.MinValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfLongMaxValueIsPassedOnWindows()
    {
        var action = () => CLongRecordTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void CanPassLongValueThroughCallbackOn64BitUnix(long value)
    {
        long Callback(long val)
        {
            return val;
        }

        CLongRecordTester.RunCallback(value, Callback).Should().Be(value);
    }

    [TestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void CanPassIntValueThroughCallbackOnWindows(int value)
    {
        long Callback(long val)
        {
            return val;
        }

        CLongRecordTester.RunCallback(value, Callback).Should().Be(value);
    }
}
