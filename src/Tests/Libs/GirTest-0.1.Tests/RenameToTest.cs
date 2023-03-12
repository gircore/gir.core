using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class RenameToTest : Test
{
    [TestMethod]
    public void FunctionsAreShadowed()
    {
        RenameToTester.Get(1).Should().Be(1);
        RenameToTester.Get(1, -1).Should().Be(0);
    }
}
