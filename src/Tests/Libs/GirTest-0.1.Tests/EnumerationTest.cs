using AwesomeAssertions;
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
        value.GetEnum<EnumTesterSimpleEnum>().Should().Be(EnumTesterSimpleEnum.Max);
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
        value.GetEnum<EnumTesterSimpleEnum>().Should().Be(EnumTesterSimpleEnum.Min);
    }

    [TestMethod]
    public void OutParameter()
    {
        EnumTester.OutParameter(out EnumTesterSimpleEnum simpleEnum);
        simpleEnum.Should().Be(EnumTesterSimpleEnum.A);
    }

    [TestMethod]
    public void RefParameter()
    {
        var simpleEnum = EnumTesterSimpleEnum.A;
        EnumTester.RefParameter(ref simpleEnum);

        simpleEnum.Should().Be(EnumTesterSimpleEnum.B);
    }
}
