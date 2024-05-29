using System;
using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class LongTest : Test
{
    [TestMethod]
    public void ShouldUseCLongCULongInInternalStructData()
    {
        var data = new GirTest.Internal.LongTesterData();
        data.Ul.Should().BeOfType<CULong>();
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
    public unsafe void GetSizeOfULShouldBeSizeOfCULong()
    {
        var sizeOfCULong = sizeof(CULong);
        var obj = new LongTester();
        obj.GetSizeofUl().Should().Be((nuint) sizeOfCULong);
    }

    [DataTestMethod]
    [DataRow(ulong.MaxValue)]
    [DataRow(ulong.MinValue)]
    public void ShouldHandleMaxMinULongValue(ulong value)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || !Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on 64 Bit Unix operating Systems");

        var obj = new LongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [DataTestMethod]
    [DataRow(uint.MaxValue)]
    [DataRow(uint.MinValue)]
    public void ShouldHandleMaxMinUIntValue(uint value)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new LongTester { Ul = value };
        obj.Ul.Should().Be(value);
    }

    [TestMethod]
    public void ShouldThrowIfValueExceedsUnsignedIntegerSize()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && Environment.Is64BitOperatingSystem)
            Assert.Inconclusive("Only supported on windows or 32 bit unix operating Systems");

        var obj = new LongTester();
        var act = () => obj.Ul = ulong.MaxValue;
        act.Should().Throw<OverflowException>();
    }
}
