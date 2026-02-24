using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PlatformStringSizedTest : Test
{
    [TestMethod]
    public void SupportsParameterInTransferNone()
    {
        PlatformStringArraySizedTester.ParameterInWithLengthParameterTransferNone(null).Should().BeNull();
        PlatformStringArraySizedTester.ParameterInWithLengthParameterTransferNone(["a", "b", "c"]).Should().Be("c");
    }

    [TestMethod]
    public void SupportsParameterInTransferFull()
    {
        PlatformStringArraySizedTester.ParameterInWithLengthParameterTransferFull(null).Should().BeNull();
        PlatformStringArraySizedTester.ParameterInWithLengthParameterTransferFull(["a", "b", "c"]).Should().Be("c");
    }

    [TestMethod]
    public void SupportsParameterOutTransferNone()
    {
        PlatformStringArraySizedTester.ParameterOutWithLengthParameterTransferNone(true, out var data1);
        data1.Should().BeNull();

        PlatformStringArraySizedTester.ParameterOutWithLengthParameterTransferNone(false, out var data2);
        data2.Should().HaveCount(2);
        data2[0].Should().Be("FOO");
        data2[1].Should().Be("BAR");
    }

    [TestMethod]
    public void SupportsParameterOutTransferFull()
    {
        PlatformStringArraySizedTester.ParameterOutWithLengthParameterTransferFull(true, out var data1);
        data1.Should().BeNull();

        PlatformStringArraySizedTester.ParameterOutWithLengthParameterTransferFull(false, out var data2);
        data2.Should().HaveCount(2);
        data2[0].Should().Be("hello");
        data2[1].Should().Be("world");
    }

    [TestMethod]
    public void SupportsParameterInOutTransferFull()
    {
        string[]? array1 = null;
        PlatformStringArraySizedTester.ParameterInoutWithLengthParameterTransferFull(ref array1);
        array1.Should().HaveCount(2);
        array1[0].Should().Be("hello");
        array1[1].Should().Be("world");

        var array2 = new[] { "the", "world", "hello" };
        PlatformStringArraySizedTester.ParameterInoutWithLengthParameterTransferFull(ref array2);

        array2.Should().HaveCount(2);
        array2[0].Should().Be("hello");
        array2[1].Should().Be("world");
    }
}
