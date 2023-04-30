using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class UnhandledExceptionTest : Test
{
    [TestMethod]
    public void SupportsSignalHandlers()
    {
        var tester = ReturningSignalTester.New();
        tester.OnReturnBool += (_, _) =>
        {
            throw new NotImplementedException();
        };

        bool exceptionCaught = false;
        GLib.UnhandledException.Raised += (_, args) =>
        {
            exceptionCaught = args.ExceptionObject is NotImplementedException;
        };

        tester.EmitReturnBool().Should().Be(false);
        exceptionCaught.Should().Be(true);
    }
}
