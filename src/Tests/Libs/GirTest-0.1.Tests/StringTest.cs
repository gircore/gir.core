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
}
