using FluentAssertions;
using GObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class EnumerationTest : Test
{
    [TestMethod]
    public void CanBeUsedInGValue()
    {
        var e = EnumTesterSimpleEnum.A;
        var value = new Value(Type.Enum);
        value.Set(e);
        var result = value.Extract<EnumTesterSimpleEnum>();
        result.Should().Be(e);
    }
}
