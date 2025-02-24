using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class StringTest : Test
{
    // This string is 4 bytes in the glib filename encoding (ISO-8859-1) that is
    // used for these tests, but is 5 bytes in UTF-8.
    private const string TestString = "a\u00f6bc";

    [TestMethod]
    public void InUtf8ParameterWithFullTransferDoNotDoubleFree()
    {
        //In case a double free bug occurs the runtime crashes and the "dotnet test" command returns an error

        StringTester.Utf8InTransferFull(TestString);
        StringTester.Utf8InNullableTransferFull(TestString);
        StringTester.Utf8InNullableTransferFull(null);
    }

    [TestMethod]
    public void InFilenameParameterWithFullTransferDoNotDoubleFree()
    {
        //In case a double free bug occurs the runtime crashes and the "dotnet test" command returns an error

        StringTester.FilenameInTransferFull(TestString);
        StringTester.FilenameInNullableTransferFull(TestString);
        StringTester.FilenameInNullableTransferFull(null);
    }

    [TestMethod]
    public void InUtf8ParameterShouldSucceed()
    {
        StringTester.Utf8In(TestString).Should().Be(TestString.Length);
        StringTester.Utf8InNullable(TestString).Should().Be(TestString.Length);
        StringTester.Utf8InNullable(null).Should().Be(-1);
    }

    [TestMethod]
    public void InFilenameParameterShouldSucceed()
    {
        StringTester.FilenameIn(TestString).Should().Be(TestString.Length);

        StringTester.FilenameInNullable(TestString).Should().Be(TestString.Length);
        StringTester.FilenameInNullable(null).Should().Be(-1);
    }

    [TestMethod]
    public void ReturnUtf8ParameterShouldSucceed()
    {
        StringTester.Utf8ReturnTransferFull(TestString).Should().Be(TestString);
        StringTester.Utf8ReturnTransferNone(TestString).Should().Be(TestString);

        StringTester.Utf8ReturnNullableTransferFull(TestString).Should().Be(TestString);
        StringTester.Utf8ReturnNullableTransferFull(null).Should().Be(null);

        StringTester.Utf8ReturnNullableTransferNone(TestString).Should().Be(TestString);
        StringTester.Utf8ReturnNullableTransferNone(null).Should().Be(null);
    }

    [TestMethod]
    public void ReturnFilenameParameterShouldSucceed()
    {
        StringTester.FilenameReturnTransferFull(TestString).Should().Be(TestString);
        StringTester.FilenameReturnTransferNone(TestString).Should().Be(TestString);

        StringTester.FilenameReturnNullableTransferFull(TestString).Should().Be(TestString);
        StringTester.FilenameReturnNullableTransferFull(null).Should().Be(null);

        StringTester.FilenameReturnNullableTransferNone(TestString).Should().Be(TestString);
        StringTester.FilenameReturnNullableTransferNone(null).Should().Be(null);
    }

    [TestMethod]
    public void ReturnStringFromCallbackShouldSucceed()
    {
        StringTester.CallbackReturnStringTransferFull(() => TestString).Should().Be(TestString.Length);
    }

    [TestMethod]
    public void UnexpectedUtf8NullThrowsNullHandleException()
    {
        Action act = () => StringTester.Utf8ReturnUnexpectedNull();
        act.Should().Throw<GLib.Internal.NullHandleException>();
    }

    [TestMethod]
    public void UnexpectedFilenameNullThrowsNullHandleException()
    {
        Action act = () => StringTester.FilenameReturnUnexpectedNull();
        act.Should().Throw<GLib.Internal.NullHandleException>();
    }

    [TestMethod]
    public void OutUtf8ParameterShouldSucceed()
    {
        StringTester.Utf8OutTransferNone(TestString, out string result);
        result.Should().Be(TestString);

        StringTester.Utf8OutOptionalTransferNone(TestString, out string result2);
        result2.Should().Be(TestString);

        StringTester.Utf8OutTransferFull(TestString, out string result3);
        result3.Should().Be(TestString);

        StringTester.Utf8OutOptionalTransferFull(TestString, out string result4);
        result4.Should().Be(TestString);

        StringTester.Utf8OutNullableTransferNone(TestString, out string? result5);
        result5.Should().Be(TestString);

        StringTester.Utf8OutNullableTransferNone(null, out string? result6);
        result6.Should().BeNull();

        StringTester.Utf8OutNullableOptionalTransferNone(TestString, out string? result7);
        result7.Should().Be(TestString);

        StringTester.Utf8OutNullableOptionalTransferNone(null, out string? result8);
        result8.Should().BeNull();

        StringTester.Utf8OutNullableTransferFull(TestString, out string? result9);
        result9.Should().Be(TestString);

        StringTester.Utf8OutNullableTransferFull(null, out string? result10);
        result10.Should().BeNull();

        StringTester.Utf8OutNullableOptionalTransferFull(TestString, out string? result11);
        result11.Should().Be(TestString);

        StringTester.Utf8OutNullableOptionalTransferFull(null, out string? result12);
        result12.Should().BeNull();
    }

    [TestMethod]
    public void OutFilenameParameterShouldSucceed()
    {
        StringTester.FilenameOutTransferNone(TestString, out string result);
        result.Should().Be(TestString);

        StringTester.FilenameOutOptionalTransferNone(TestString, out string result2);
        result2.Should().Be(TestString);

        StringTester.FilenameOutTransferFull(TestString, out string result3);
        result3.Should().Be(TestString);

        StringTester.FilenameOutOptionalTransferFull(TestString, out string result4);
        result4.Should().Be(TestString);

        StringTester.FilenameOutNullableTransferNone(TestString, out string? result5);
        result5.Should().Be(TestString);

        StringTester.FilenameOutNullableTransferNone(null, out string? result6);
        result6.Should().BeNull();

        StringTester.FilenameOutNullableOptionalTransferNone(TestString, out string? result7);
        result7.Should().Be(TestString);

        StringTester.FilenameOutNullableOptionalTransferNone(null, out string? result8);
        result8.Should().BeNull();

        StringTester.FilenameOutNullableTransferFull(TestString, out string? result9);
        result9.Should().Be(TestString);

        StringTester.FilenameOutNullableTransferFull(null, out string? result10);
        result10.Should().BeNull();

        StringTester.FilenameOutNullableOptionalTransferFull(TestString, out string? result11);
        result11.Should().Be(TestString);

        StringTester.FilenameOutNullableOptionalTransferFull(null, out string? result12);
        result12.Should().BeNull();
    }
}
