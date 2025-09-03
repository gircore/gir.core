using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class CULongRecordTest : Test
{
    [TestMethod]
    public void ShouldUseCULongInInternalStructData()
    {
        var data = new GirTest.Internal.CULongRecordTesterData();
        data.Ul.Should().BeOfType<CULong>();
    }

    [TestMethod]
    public unsafe void GetSizeOfLShouldBeSizeOfCULong()
    {
        var sizeOfCULong = sizeof(CULong);
        var obj = new CULongRecordTester();
        obj.GetSizeofUl().Should().Be((nuint) sizeOfCULong);
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    [PlatformCondition(Platform.Unix64)]
    public void ShouldHandleMaxULongValue(ulong value)
    {
        var obj = new CULongRecordTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldHandleMaxMinUIntValue(uint value)
    {
        var obj = new CULongRecordTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ShouldThrowIfValueExceedsUIntegerSize()
    {
        var obj = new CULongRecordTester();
        var act = () => obj.Ul = ulong.MaxValue;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void ReturnsMaxULongValueOn64BitUnix()
    {
        CULongRecordTester.GetMaxUnsignedLongValue().Should().Be(ulong.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ReturnsMaxULongValueOnWindows()
    {
        CULongRecordTester.GetMaxUnsignedLongValue().Should().Be(uint.MaxValue);
    }

    [TestMethod]
    [PlatformCondition(Platform.Unix64)]
    public void SetsMaxULongValueOn64BitUnix()
    {
        CULongRecordTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void SetsMaxULongValueOnWindows()
    {
        CULongRecordTester.IsMaxUnsignedLongValue(uint.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    [PlatformCondition(Platform.Windows | Platform.Unix32)]
    public void ThrowsOverflowExceptionIfULongMaxValueIsPassedOnWindows()
    {
        var action = () => CULongRecordTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
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

        CULongRecordTester.RunCallback(value, Callback).Should().Be(value);
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

        CULongRecordTester.RunCallback(value, Callback).Should().Be(value);
    }
}
