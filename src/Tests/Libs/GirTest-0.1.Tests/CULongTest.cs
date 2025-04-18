using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class CULongTest : Test
{
    [TestMethod]
    public void ShouldUseCULongInInternalStructData()
    {
        var data = new GirTest.Internal.CULongTesterData();
        data.Ul.Should().BeOfType<CULong>();
    }

    [TestMethod]
    public unsafe void GetSizeOfLShouldBeSizeOfCULong()
    {
        var sizeOfCULong = sizeof(CULong);
        var obj = new CULongTester();
        obj.GetSizeofUl().Should().Be((nuint) sizeOfCULong);
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void ShouldHandleMaxULongValue(ulong value)
    {
        var obj = new CULongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldHandleMaxMinUIntValue(uint value)
    {
        var obj = new CULongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldThrowIfValueExceedsUIntegerSize()
    {
        var obj = new CULongTester();
        var act = () => obj.Ul = ulong.MaxValue;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMaxULongValueOn64BitUnix()
    {
        CULongTester.GetMaxUnsignedLongValue().Should().Be(ulong.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMaxULongValueOnWindows()
    {
        CULongTester.GetMaxUnsignedLongValue().Should().Be(uint.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMaxULongValueOn64BitUnix()
    {
        CULongTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMaxULongValueOnWindows()
    {
        CULongTester.IsMaxUnsignedLongValue(uint.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfULongMaxValueIsPassedOnWindows()
    {
        var action = () => CULongTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void CanPassULongValueThroughCallbackOn64BitUnix(ulong value)
    {
        ulong Callback(ulong val)
        {
            return val;
        }

        CULongTester.RunCallback(value, Callback).Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void CanPassIntValueThroughCallbackOnWindows(uint value)
    {
        ulong Callback(ulong val)
        {
            return val;
        }

        CULongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
