using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class AliasTest : Test
{
    [TestMethod]
    public void CanBeCastedExplicitlyToBaseType()
    {
        var intAlias = new IntAlias(42);
        ((int) intAlias).Should().Be(42);
    }

    [TestMethod]
    public void CanBeCastedImplicitlyToBaseType()
    {
        int integer = new IntAlias(42);
        integer.Should().Be(42);
    }

    [TestMethod]
    public void CanBeCastedImplicitlyFromBaseType()
    {
        IntAlias intAlias = 42;
        intAlias.Should().Be(new IntAlias(42));
    }

    [TestMethod]
    public void CanBeInParameter()
    {
        AliasTester.ToInt(new IntAlias(42)).Should().Be(42);
    }

    [TestMethod]
    public void CanBeReturnValue()
    {
        AliasTester.ToIntAlias(42).Should().Be(new IntAlias(42));
    }

    [TestMethod]
    public void CanBeOutParameter()
    {
        AliasTester.AliasOut(out var alias);
        alias.Should().Be(new IntAlias(42));
    }

    [TestMethod]
    public void CanBeInOutParameter()
    {
        var intAlias = new IntAlias(21);
        AliasTester.AliasInOut(ref intAlias);

        intAlias.Should().Be(new IntAlias(42));
    }

    [TestMethod]
    public void ConstantShouldBeOfTypeInt()
    {
        Constants.INT_ALIAS_ZERO.Should().BeOfType(typeof(int));
    }

    [TestMethod]
    public void ConstantsCanBeUsedInSwitchStatement()
    {
        var intAlias = new IntAlias(0);
        bool result;

        switch (intAlias)
        {
            case Constants.INT_ALIAS_ZERO:
                result = true;
                break;
            default:
                result = false;
                break;
        }

        result.Should().BeTrue();
    }

    [TestMethod]
    public void ConstantsCanBeUsedInSwitchExpressions()
    {
        var intAlias = new IntAlias(0);
        var result = (int) intAlias switch
        {
            Constants.INT_ALIAS_ZERO => true,
            _ => false
        };
        result.Should().BeTrue();
    }

    [TestMethod]
    public void CanConvertPointerAliasToPointer()
    {
        AliasTester.ToPointer(new PointerAlias((IntPtr) 42)).Should().Be((IntPtr) 42);
    }

    [TestMethod]
    public void CanConvertPointerToPointerAlias()
    {
        AliasTester.ToPointerAlias((IntPtr) 42).Should().Be(new PointerAlias((IntPtr) 42));
    }

    [TestMethod]
    public void CanReadArrayTransferFull()
    {
        var array = AliasTester.GetArrayTransferFull();
        array.Length.Should().Be(3);
        array[0].Should().Be(new IntAlias(1));
        array[1].Should().Be(new IntAlias(2));
        array[2].Should().Be(new IntAlias(3));
    }
}
