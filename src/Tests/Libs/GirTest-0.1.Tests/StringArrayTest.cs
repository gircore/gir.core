using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class StringArrayTest : Test
{
    [TestMethod]
    public void Utf8ReturnNullTerminatedStringArrayTransferNone()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.Utf8ReturnTransferNone().Should().BeEquivalentTo(array);
    }

    [TestMethod]
    public void Utf8ReturnNullTerminatedStringArrayTransferNoneNullable()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.Utf8ReturnTransferNoneNullable(false).Should().BeEquivalentTo(array);
        StringArrayTester.Utf8ReturnTransferNoneNullable(true).Should().BeNull();
    }

    [TestMethod]
    public void Utf8ParameterNullTerminatedStringArrayTransferNone()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.Utf8ReturnElementParameterNullTerminatedTransferNone(array, 0).Should().Be(array[0]);
        StringArrayTester.Utf8ReturnElementParameterNullTerminatedTransferNone(array, 1).Should().Be(array[1]);
    }

    [TestMethod]
    public void Utf8ParameterNullTerminatedStringArrayTransferNoneNullable()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.Utf8ReturnElementParameterNullTerminatedTransferNoneNullable(array, 0).Should().Be(array[0]);
        StringArrayTester.Utf8ReturnElementParameterNullTerminatedTransferNoneNullable(array, 1).Should().Be(array[1]);
        StringArrayTester.Utf8ReturnElementParameterNullTerminatedTransferNoneNullable(null, 1).Should().BeNull();
    }

    [TestMethod]
    public void Utf8ParameterTransferNoneNullableWithSize()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.Utf8ReturnElementParameterTransferNoneNullableWithSize(array, 0).Should().Be(array[0]);
        StringArrayTester.Utf8ReturnElementParameterTransferNoneNullableWithSize(array, 1).Should().Be(array[1]);
        StringArrayTester.Utf8ReturnElementParameterTransferNoneNullableWithSize(null, 1).Should().BeNull();
    }

    [TestMethod]
    public void PlatformReturnNullTerminatedStringArrayTransferNone()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.FilenameReturnTransferNone().Should().BeEquivalentTo(array);
    }

    [TestMethod]
    public void PlatformReturnNullTerminatedStringArrayTransferNoneNullable()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.FilenameReturnTransferNoneNullable(false).Should().BeEquivalentTo(array);
        StringArrayTester.FilenameReturnTransferNoneNullable(true).Should().BeNull();
    }

    [TestMethod]
    public void PlatformParameterNullTerminatedStringArrayTransferNone()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.FilenameReturnElementParameterNullTerminatedTransferNone(array, 0).Should().Be(array[0]);
        StringArrayTester.FilenameReturnElementParameterNullTerminatedTransferNone(array, 1).Should().Be(array[1]);
    }

    [TestMethod]
    public void PlatformParameterNullTerminatedStringArrayTransferNoneNullable()
    {
        var array = new[] { "FOO", "BAR" };
        StringArrayTester.FilenameReturnElementParameterNullTerminatedTransferNoneNullable(array, 0).Should().Be(array[0]);
        StringArrayTester.FilenameReturnElementParameterNullTerminatedTransferNoneNullable(array, 1).Should().Be(array[1]);
        StringArrayTester.FilenameReturnElementParameterNullTerminatedTransferNoneNullable(null, 1).Should().BeNull();
    }
}
