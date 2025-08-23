using System;
using System.Runtime.InteropServices;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class BytesTest : Test
{
    [TestMethod]
    public void CanReturnAllData()
    {
        var data = new byte[] { 0x01, 0x02, 0x03 };
        var bytes = BytesTester.ReturnBytes(data[0], data[1], data[2]);

        var span = bytes.GetRegionSpan<byte>(0, (nuint) data.Length);
        span[0].Should().Be(data[0]);
        span[1].Should().Be(data[1]);
        span[2].Should().Be(data[2]);
        span.Length.Should().Be(3);
    }

    [TestMethod]
    public void CanReturnFromOffset()
    {
        var data = new byte[] { 0x01, 0x02, 0x03 };
        var bytes = BytesTester.ReturnBytes(data[0], data[1], data[2]);

        var span = bytes.GetRegionSpan<byte>(1, 2);
        span[0].Should().Be(data[1]);
        span[1].Should().Be(data[2]);
        span.Length.Should().Be(2);
    }

    [TestMethod]
    public void RequestingOutOfBoundsDataResultsInEmptySpan()
    {
        var bytes = BytesTester.ReturnBytes(1, 2, 3);
        var span = bytes.GetRegionSpan<byte>(0, 4); //Count 4 is out of bounds for 3 bytes
        span.IsEmpty.Should().BeTrue();
    }
}
