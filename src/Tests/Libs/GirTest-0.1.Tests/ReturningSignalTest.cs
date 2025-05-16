using Combinatorial.MSTest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class ReturningSignalTest : Test
{
    [DataTestMethod]
    [CombinatorialData]
    public void SupportsReturningBool(bool returnValue)
    {
        var tester = ReturningSignalTester.New();
        tester.OnReturnBool += (_, _) => returnValue;

        tester.EmitReturnBool().Should().Be(returnValue);
    }
}
