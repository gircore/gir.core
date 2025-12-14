using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class Utf8StringSizedTest : Test
{
    [TestMethod]
    public void SupportsParameterInTransferNone()
    {
        Utf8StringArraySizedTester.ParameterInWithLengthParameterTransferNone(null).Should().BeNull();
        Utf8StringArraySizedTester.ParameterInWithLengthParameterTransferNone(["a", "b", "c"]).Should().Be("c");
    }

    [TestMethod]
    public void SupportsParameterInTransferFull()
    {
        Utf8StringArraySizedTester.ParameterInWithLengthParameterTransferFull(null).Should().BeNull();
        Utf8StringArraySizedTester.ParameterInWithLengthParameterTransferFull(["a", "b", "c"]).Should().Be("c");
    }

    [TestMethod]
    public void SupportsParameterOutTransferNone()
    {
        Utf8StringArraySizedTester.ParameterOutWithLengthParameterTransferNone(true, out var data1);
        data1.Should().BeNull();

        Utf8StringArraySizedTester.ParameterOutWithLengthParameterTransferNone(false, out var data2);
        data2.Should().HaveCount(2);
        data2[0].Should().Be("FOO");
        data2[1].Should().Be("BAR");
    }

    [TestMethod]
    public void SupportsParameterOutTransferFull()
    {
        Utf8StringArraySizedTester.ParameterOutWithLengthParameterTransferFull(true, out var data1);
        data1.Should().BeNull();

        Utf8StringArraySizedTester.ParameterOutWithLengthParameterTransferFull(false, out var data2);
        data2.Should().HaveCount(2);
        data2[0].Should().Be("hello");
        data2[1].Should().Be("world");
    }

    [TestMethod]
    public void SupportsParameterInOutTransferFull()
    {
        string[]? array1 = null;
        Utf8StringArraySizedTester.ParameterInoutWithLengthParameterTransferFull(ref array1);
        array1.Should().HaveCount(2);
        array1[0].Should().Be("hello");
        array1[1].Should().Be("world");

        var array2 = new[] { "the", "world", "hello" };
        Utf8StringArraySizedTester.ParameterInoutWithLengthParameterTransferFull(ref array2);

        array2.Should().HaveCount(2);
        array2[0].Should().Be("hello");
        array2[1].Should().Be("world");
    }
}
