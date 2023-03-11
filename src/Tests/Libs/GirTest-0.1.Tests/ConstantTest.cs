using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ConstantTest : Test
{
    [TestMethod]
    public void ConstantValuesShouldBeConst()
    {
        // Verify that the constants are 'const' values, not just 'static'.
        const int int_val = GirTest.Constants.INTVAL;
        int_val.Should().Be(42);

        const string str_val = GirTest.Constants.STRVAL;
        str_val.Should().Be("string value");
    }
}
