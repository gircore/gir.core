using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class PropertyTest : Test
{
    [DataTestMethod]
    [DataRow("NewTitle")]
    [DataRow("Some Text With Unicode ☀🌙🌧")]
    public void TestStringProperty(string str)
    {
        var obj = PropertyTester.New();
        obj.StringValue = str;

        obj.StringValue.Should().Be(str);
    }

    [TestMethod]
    public void TestNullStringProperty()
    {
        var text = "text";
        var obj = PropertyTester.New();
        obj.StringValue = text;
        obj.StringValue.Should().Be(text);
        obj.StringValue = null;
        obj.StringValue.Should().BeNull();
    }

    [TestMethod]
    public void PropertyNamedLikeClass()
    {
        //Properties named like a class are suffixed with an underscore.
        var obj = PropertyTester.New();
        obj.PropertyTester_ = "test";
    }
}
