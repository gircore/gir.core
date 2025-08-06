using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests.Classes;

[TestClass, TestCategory("UnitTest")]
public class InterfaceTests : Test
{
    [TestMethod]
    public void InterfaceShouldImplementIDisposable()
    {
        typeof(TypePlugin).Should().Implement<IDisposable>();
    }
}
