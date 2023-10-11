using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PlatformStringArrayNullTerminated : Test
{
    [TestMethod]
    public void SupportsParameterTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferNone(data);

        result.Should().Be(data[1]);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferFull(data);

        result.Should().Be(data[1]);
    }

    [TestMethod]
    public void SupportsParameterTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferNoneNullable(data);
        result.Should().Be(data[1]);

        result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferNoneNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferFullNullable(data);
        result.Should().Be(data[1]);

        result = PlatformStringArrayNullTerminatedTester.GetLastElementTransferFullNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferFull(data);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferNone(data);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferContainer()
    {
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferContainer("foo", "bar");

        result[0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferFullNullable(data);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        result = PlatformStringArrayNullTerminatedTester.ReturnTransferFullNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferNoneNullable(data);

        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        result = PlatformStringArrayNullTerminatedTester.ReturnTransferNoneNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferContainerNullable()
    {
        var result = PlatformStringArrayNullTerminatedTester.ReturnTransferContainerNullable("foo", "bar");

        result![0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);

        result = PlatformStringArrayNullTerminatedTester.ReturnTransferContainerNullable(null, null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferFull(data, out var result);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferNone(data, out var result);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferContainer()
    {
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferContainer("foo", "bar", out var result);

        result[0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferFullNullable(data, out var result);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        PlatformStringArrayNullTerminatedTester.ParameterOutTransferFullNullable(null, out var result2);
        result2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferNoneNullable(data, out var result);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        PlatformStringArrayNullTerminatedTester.ParameterOutTransferNoneNullable(null, out var result2);
        result2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferContainerNullable()
    {
        PlatformStringArrayNullTerminatedTester.ParameterOutTransferContainerNullable("foo", "bar", out var result);
        result![0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);

        PlatformStringArrayNullTerminatedTester.ParameterOutTransferContainerNullable(null, null, out var result2);
        result2.Should().BeNull();
    }
}
