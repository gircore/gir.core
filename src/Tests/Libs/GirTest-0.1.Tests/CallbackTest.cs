using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class CallbackTest : Test
{
    [TestMethod]
    public void SupportsNotifiedCallbacks()
    {
        var tester = CallbackTester.New();

        tester.SetNotifiedCallback((val) => 2 * val);

        tester.RunNotifiedCallback(2, done: false).Should().Be(4);
        tester.RunNotifiedCallback(3, done: false).Should().Be(6);
        tester.RunNotifiedCallback(4, done: true).Should().Be(8);
        tester.RunNotifiedCallback(5, done: false).Should().Be(-1);
    }
}
