using Combinatorial.MSTest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class VariantTest : Test
{
    [DataTestMethod]
    [CombinatorialData]
    public void CanCreateBool(bool value)
    {
        var variant = Variant.NewBoolean(value);
        variant.GetBoolean().Should().Be(value);
    }

    [TestMethod]
    public void CanCreateInt32()
    {
        int value = 5;
        var variant = Variant.NewInt32(value);

        variant.GetInt32().Should().Be(value);
    }

    [TestMethod]
    public void CanCreateUInt32()
    {
        uint value = 5;
        var variant = Variant.NewUint32(value);

        variant.GetUint32().Should().Be(value);
    }

    [TestMethod]
    public void CanCreateString()
    {
        string value = "test";
        var variant = Variant.NewString(value);

        variant.GetString(out var length).Should().Be(value);
        length.Should().Be((nuint) value.Length);
    }

    [TestMethod]
    public void CanCreateStringArray()
    {
        var data = new[] { "Str1", "Str2", "Str3" };
        var variant = Variant.NewStrv(data, data.Length);
        variant.Print(false).Should().Be("['Str1', 'Str2', 'Str3']");

        var strv = variant.GetStrv(out var length);
        strv[0].Should().Be("Str1");
        strv[1].Should().Be("Str2");
        strv[2].Should().Be("Str3");
        length.Should().Be(3);

        var str1 = variant.GetChildValue(0);
        str1.GetString(out length).Should().Be("Str1");
        length.Should().Be(4);

        var str2 = variant.GetChildValue(1);
        str2.GetString(out length).Should().Be("Str2");
        length.Should().Be(4);

        var str3 = variant.GetChildValue(2);
        str3.GetString(out length).Should().Be("Str3");
        length.Should().Be(4);
    }

    [TestMethod]
    public void CanCreateTuple()
    {
        var tuple = Variant.NewTuple(new[] { Variant.NewInt32(1), Variant.NewInt32(2) });
        tuple.Print(false).Should().Be("(1, 2)");

        var one = tuple.GetChildValue(0);
        one.GetInt32().Should().Be(1);

        var two = tuple.GetChildValue(1);
        two.GetInt32().Should().Be(2);
    }

    [TestMethod]
    public void CanCreateArrayOfInt32()
    {
        var array = Variant.NewArray(null, new[] { Variant.NewInt32(1), Variant.NewInt32(2) });
        array.Print(false).Should().Be("[1, 2]");

        var one = array.GetChildValue(0);
        one.GetInt32().Should().Be(1);

        var two = array.GetChildValue(1);
        two.GetInt32().Should().Be(2);
    }

    [TestMethod]
    public void CanCreateDictionaryOfInt32String()
    {
        var dictionary = Variant.NewArray(null, new[] { Variant.NewDictEntry(Variant.NewInt32(1), Variant.NewString("test")) });
        dictionary.Print(true).Should().Be("{1: 'test'}");
        dictionary.GetTypeString().Should().Be("a{is}");

        var value = dictionary.GetChildValue(0);
        value.GetChildValue(0).GetInt32().Should().Be(1);
        value.GetChildValue(1).GetString(out var length).Should().Be("test");
    }

    [TestMethod]
    public void DisposeClosesHandle()
    {
        var variant = Variant.NewString("Test");
        variant.Handle.IsClosed.Should().BeFalse();
        variant.Dispose();
        variant.Handle.IsClosed.Should().BeTrue();
    }

    [TestMethod]
    public void CanUseVariantIter()
    {
        var builder = VariantBuilder.New(VariantType.New("as"));
        builder.AddValue(Variant.NewString("a"));
        builder.AddValue(Variant.NewString("test"));
        var variant = builder.End();

        var iter = new VariantIter();
        var numberItems = iter.Init(variant);

        numberItems.Should().Be(2);

        var first = iter.NextValue()!;
        first.GetString(out _).Should().Be("a");

        var second = iter.NextValue()!;
        second.GetString(out _).Should().Be("test");
    }
}
