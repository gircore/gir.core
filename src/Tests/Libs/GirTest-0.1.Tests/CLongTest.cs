using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class CLongTest : Test
{
    [TestMethod]
    public void ShouldUseCLongInInternalStructData()
    {
        var data = new GirTest.Internal.CLongTesterData();
        data.L.Should().BeOfType<CLong>();
    }

    [TestMethod]
    public unsafe void GetSizeOfLShouldBeSizeOfCLong()
    {
        var sizeOfCLong = sizeof(CLong);
        var obj = new CLongTester();
        obj.GetSizeofL().Should().Be((nuint) sizeOfCLong);
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void ShouldHandleMaxMinLongValue(long value)
    {
        var obj = new CLongTester { L = value };
        obj.L.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldHandleMaxMinIntValue(int value)
    {
        var obj = new CLongTester { L = value };
        obj.L.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldThrowIfValueExceedsIntegerSize(long value)
    {
        var obj = new CLongTester();
        var act = () => obj.L = value;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMaxLongValueOn64BitUnix()
    {
        CLongTester.GetMaxLongValue().Should().Be(long.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMaxLongValueOnWindows()
    {
        CLongTester.GetMaxLongValue().Should().Be(int.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMinLongValueOn64BitUnix()
    {
        CLongTester.GetMinLongValue().Should().Be(long.MinValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMinLongValueOnWindows()
    {
        CLongTester.GetMinLongValue().Should().Be(int.MinValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMaxLongValueOn64BitUnix()
    {
        CLongTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMaxLongValueOnWindows()
    {
        CLongTester.IsMaxLongValue(int.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMinLongValueOn64BitUnix()
    {
        CLongTester.IsMinLongValue(long.MinValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMinLongValueOnWindows()
    {
        CLongTester.IsMinLongValue(int.MinValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfLongMinValueIsPassedOnWindows()
    {
        var action = () => CLongTester.IsMinLongValue(long.MinValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfLongMaxValueIsPassedOnWindows()
    {
        var action = () => CLongTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void CanPassLongValueThroughCallbackOn64BitUnix(long value)
    {
        long Callback(long val)
        {
            return val;
        }

        CLongTester.RunCallback(value, Callback).Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void CanPassIntValueThroughCallbackOnWindows(int value)
    {
        long Callback(long val)
        {
            return val;
        }

        CLongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
