using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ErrorTest : Test
{
    [TestMethod]
    public void ConstructorCanFail()
    {
        var obj = ErrorTester.NewCanFail(fail: false);
        obj.Should().NotBeNull();

        Action action = () => ErrorTester.NewCanFail(true);
        action.Should().Throw<Exception>().And.Message.Should().Contain("Constructor failed");
    }

    [TestMethod]
    public void MethodCanFail()
    {
        var obj = ErrorTester.NewCanFail(fail: false);
        obj.MethodCanFail(false).Should().Be(true);

        Action action = () => obj.MethodCanFail(true);
        action.Should().Throw<Exception>().And.Message.Should().Contain("Method failed");
    }

    [TestMethod]
    public void FunctionCanFail()
    {
        ErrorTester.FunctionCanFail(false).Should().Be(true);

        Action action = () => ErrorTester.FunctionCanFail(true);
        action.Should().Throw<Exception>().And.Message.Should().Contain("Function failed");
    }
}
