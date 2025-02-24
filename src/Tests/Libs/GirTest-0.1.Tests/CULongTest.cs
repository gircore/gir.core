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
    public void ShouldHandleMaxULongValue(ulong value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        var obj = new CULongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    public void ShouldHandleMaxMinUIntValue(uint value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new CULongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [TestMethod]
    public void ShouldThrowIfValueExceedsUIntegerSize()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new CULongTester();
        var act = () => obj.Ul = ulong.MaxValue;
        act.Should().Throw<OverflowException>();
    }

    [TestMethod]
    public void ReturnsMaxULongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        CULongTester.GetMaxUnsignedLongValue().Should().Be(ulong.MaxValue);
    }

    [TestMethod]
    public void ReturnsMaxULongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        CULongTester.GetMaxUnsignedLongValue().Should().Be(uint.MaxValue);
    }

    [TestMethod]
    public void SetsMaxULongValueOn64BitUnix()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        CULongTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void SetsMaxULongValueOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        CULongTester.IsMaxUnsignedLongValue(uint.MaxValue).Should().BeTrue();
    }

    [TestMethod]
    public void ThrowsOverflowExceptionIfULongMaxValueIsPassedOnWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var action = () => CULongTester.IsMaxUnsignedLongValue(ulong.MaxValue).Should().BeTrue();
        action.Should().Throw<OverflowException>();
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void CanPassULongValueThroughCallbackOn64BitUnix(ulong value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        ulong Callback(ulong val)
        {
            return val;
        }

        CULongTester.RunCallback(value, Callback).Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    public void CanPassIntValueThroughCallbackOnWindows(uint value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        ulong Callback(ulong val)
        {
            return val;
        }

        CULongTester.RunCallback(value, Callback).Should().Be(value);
    }
}
