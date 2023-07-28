using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class FundamentalTest : Test
{
    [TestMethod]
    public void EnsureNullIsReturned()
    {
        InstantiatableFundamental? obj = FundamentalTester.CreateFundamental(returnNull: true);
        obj.Should().BeNull();
    }

    [TestMethod]
    public void EnsureNullIsNotReturned()
    {
        InstantiatableFundamental? obj = FundamentalTester.CreateFundamental(returnNull: false);
        obj.Should().NotBeNull();
        obj!.Unref();
    }
}
