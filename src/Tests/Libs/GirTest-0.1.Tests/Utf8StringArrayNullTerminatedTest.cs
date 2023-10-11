using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class Utf8StringArrayNullTerminated : Test
{
    [TestMethod]
    public void SupportsParameterTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferNone(data);

        result.Should().Be(data[1]);
    }

    [TestMethod]
    public void SupportsParameterTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferFull(data);

        result.Should().Be(data[1]);
    }

    [TestMethod]
    public void SupportsParameterTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferNoneNullable(data);
        result.Should().Be(data[1]);

        result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferNoneNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferFullNullable(data);
        result.Should().Be(data[1]);

        result = Utf8StringArrayNullTerminatedTester.GetLastElementTransferFullNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferFull(data);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferNone(data);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferContainer()
    {
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferContainer("foo", "bar");

        result[0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsReturnTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferFullNullable(data);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        result = Utf8StringArrayNullTerminatedTester.ReturnTransferFullNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferNoneNullable(data);

        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        result = Utf8StringArrayNullTerminatedTester.ReturnTransferNoneNullable(null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsReturnTransferContainerNullable()
    {
        var result = Utf8StringArrayNullTerminatedTester.ReturnTransferContainerNullable("foo", "bar");

        result![0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);

        result = Utf8StringArrayNullTerminatedTester.ReturnTransferContainerNullable(null, null);
        result.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferFull()
    {
        var data = new[] { "Str1", "Str2" };
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferFull(data, out var result);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferNone()
    {
        var data = new[] { "Str1", "Str2" };
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferNone(data, out var result);

        result[0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferContainer()
    {
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferContainer("foo", "bar", out var result);

        result[0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);
    }

    [TestMethod]
    public void SupportsParameterOutputTransferFullNullable()
    {
        var data = new[] { "Str1", "Str2" };
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferFullNullable(data, out var result);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        Utf8StringArrayNullTerminatedTester.ParameterOutTransferFullNullable(null, out var result2);
        result2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferNoneNullable()
    {
        var data = new[] { "Str1", "Str2" };
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferNoneNullable(data, out var result);
        result![0].Should().Be(data[0]);
        result[1].Should().Be(data[1]);
        result.Length.Should().Be(2);

        Utf8StringArrayNullTerminatedTester.ParameterOutTransferNoneNullable(null, out var result2);
        result2.Should().BeNull();
    }

    [TestMethod]
    public void SupportsParameterOutputTransferContainerNullable()
    {
        Utf8StringArrayNullTerminatedTester.ParameterOutTransferContainerNullable("foo", "bar", out var result);
        result![0].Should().Be("foo");
        result[1].Should().Be("bar");
        result.Length.Should().Be(2);

        Utf8StringArrayNullTerminatedTester.ParameterOutTransferContainerNullable(null, null, out var result2);
        result2.Should().BeNull();
    }
}
