using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class UnhandledExceptionTest : Test
{

    //Important: Be aware that an unhandled exception handler is
    //set globally. In case of unit tests being executed in
    //parallel this could lead to unintended side effects.

    [TestMethod]
    public void SupportsSignalHandlers()
    {
        var tester = ReturningSignalTester.New();
        tester.OnReturnBool += (_, _) =>
        {
            throw new NotImplementedException();
        };

        bool exceptionCaught = false;
        GLib.UnhandledException.SetHandler(e =>
        {
            exceptionCaught = e is NotImplementedException;
        });

        tester.EmitReturnBool().Should().Be(false);
        exceptionCaught.Should().Be(true);
    }
}
