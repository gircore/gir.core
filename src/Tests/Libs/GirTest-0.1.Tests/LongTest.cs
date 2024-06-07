using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class LongTest : Test
{
    [TestMethod]
    public void ShouldUseCLongInInternalStructData()
    {
        var data = new GirTest.Internal.LongTesterData();
        data.L.Should().BeOfType<CLong>();
    }

    [TestMethod]
    public unsafe void GetSizeOfLShouldBeSizeOfCLong()
    {
        var sizeOfCLong = sizeof(CLong);
        var obj = new LongTester();
        obj.GetSizeofL().Should().Be((nuint) sizeOfCLong);
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void ShouldHandleMaxMinLongValue(long value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        var obj = new LongTester { L = value };
        obj.L.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    public void ShouldHandleMaxMinIntValue(int value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new LongTester { L = value };
        obj.L.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void ShouldThrowIfValueExceedsIntegerSize(long value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new LongTester();
        var act = () => obj.L = value;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    public void ReturnsMaxLongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        LongTester.GetMaxLongValue().Should().Be(long.MaxValue);
    }

    [TestMethod]
    public void ReturnsMaxLongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        LongTester.GetMaxLongValue().Should().Be(int.MaxValue);
    }

    [TestMethod]
    public void ReturnsMinLongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        LongTester.GetMinLongValue().Should().Be(long.MinValue);
    }

    [TestMethod]
    public void ReturnsMinLongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        LongTester.GetMinLongValue().Should().Be(int.MinValue);
    }

    [TestMethod]
    public void SetsMaxLongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        LongTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMaxLongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        LongTester.IsMaxLongValue(int.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMinLongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        LongTester.IsMinLongValue(long.MinValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMinLongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        LongTester.IsMinLongValue(int.MinValue).Should().BeTrue();
    }

    [TestMethod]
    public void ThrowsOverflowExceptionIfLongMinValueIsPassedOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var action = () => LongTester.IsMinLongValue(long.MinValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [TestMethod]
    public void ThrowsOverflowExceptionIfLongMaxValueIsPassedOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var action = () => LongTester.IsMaxLongValue(long.MaxValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [DataTestMethod]
    [DataRow(long.MaxValue)]
    [DataRow(long.MinValue)]
    public void CanPassLongValueThroughCallbackOn64BitUnix(long value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        long Callback(long val)
        {
            return val;
        }

        LongTester.RunCallback(value, Callback).Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(int.MaxValue)]
    [DataRow(int.MinValue)]
    public void CanPassIntValueThroughCallbackOnWindows(int value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        long Callback(long val)
        {
            return val;
        }

        LongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
