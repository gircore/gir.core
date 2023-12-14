using FluentAssertions;
using GObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class EnumerationTest : Test
{
    [TestMethod]
    public void CanUseMaxInGValue()
    {
        var max = (int) EnumTesterSimpleEnum.Max;
        max.Should().Be(0x7FFFFFFF);

        var value = new Value(Type.Enum);
        value.Set(EnumTesterSimpleEnum.Max);

        value.Extract<EnumTesterSimpleEnum>().Should().Be(EnumTesterSimpleEnum.Max);
        value.GetEnum().Should().Be(0x7FFFFFFF);
    }

    [TestMethod]
    public void CanUseMinInGValue()
    {
        var min = (int) EnumTesterSimpleEnum.Min;
        min.Should().Be(1 << 31);

        var value = new Value(Type.Enum);
        value.Set(EnumTesterSimpleEnum.Min);

        value.Extract<EnumTesterSimpleEnum>().Should().Be(EnumTesterSimpleEnum.Min);
        value.GetEnum().Should().Be(1 << 31);
    }
}
