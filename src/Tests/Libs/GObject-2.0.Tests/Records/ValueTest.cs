using System.Runtime.InteropServices;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GObject.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class ValueTest : Test
{
    [DataTestMethod]
    [DataRow(5)]
    [DataRow(true)]
    [DataRow("TestString")]
    [DataRow(7u)]
    [DataRow(2.0)]
    [DataRow(2.0f)]
    public void ValueFromDataShouldContainGivenData(object data)
    {
        var v = Value.From(data);
        v.Extract().Should().Be(data);
    }

    [DataTestMethod]
    [DataRow("Hello", Internal.BasicType.String)]
    [DataRow(true, Internal.BasicType.Boolean)]
    [DataRow(1.5, Internal.BasicType.Double)]
    [DataRow(1.5f, Internal.BasicType.Float)]
    [DataRow(-7, Internal.BasicType.Int)]
    [DataRow(7u, Internal.BasicType.UInt)]
    [DataRow(77L, Internal.BasicType.Long)]
    public void ValueContainsExpectedBasicType(object data, Internal.BasicType basicType)
    {
        var v = Value.From(data);
        Internal.ValueData str = Marshal.PtrToStructure<Internal.ValueData>(v.Handle.DangerousGetHandle());
        str.GType.Should().Be((nuint) basicType);
        Internal.Functions.TypeCheckValue(v.Handle).Should().Be(true);
        Internal.Functions.TypeCheckValueHolds(v.Handle, str.GType).Should().BeTrue();
        Internal.Functions.TypeCheckValueHolds(v.Handle, (nuint) basicType).Should().BeTrue();
    }

    [TestMethod]
    public void DisposeShouldFreeUnmanagedMemory()
    {
        var value = 1;
        var v = Value.From(value);
        var ptr = v.Handle.DangerousGetHandle();

        var d1 = Marshal.PtrToStructure<Internal.ValueData>(ptr);
        d1.Data[0].VInt.Should().Be(value);

        v.Dispose();

        //Try to read the data again despite the fact that the value is already disposed
        //If the value changed we can assume that the memory got freed.
        var d2 = Marshal.PtrToStructure<Internal.ValueData>(ptr);
        d2.Data[0].VInt.Should().NotBe(value);
    }
}
