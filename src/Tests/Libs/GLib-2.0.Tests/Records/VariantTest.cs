using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class VariantTest : Test
{
    [TestMethod]
    public void CanCreateInt()
    {
        int value = 5;
        var variant = Variant.Create(value);

        variant.GetInt().Should().Be(value);
    }

    [TestMethod]
    public void CanCreateString()
    {
        string value = "test";
        var variant = Variant.Create(value);

        variant.GetString().Should().Be(value);
    }

    [TestMethod]
    public void CanCreateStringArray()
    {
        var variant = Variant.Create("Str1", "Str2", "Str3");
        variant.Print(false).Should().Be("['Str1', 'Str2', 'Str3']");
    }

    [TestMethod]
    public void DisposeClosesHandle()
    {
        var variant = Variant.Create("Test");
        variant.Handle.IsClosed.Should().BeFalse();
        variant.Dispose();
        variant.Handle.IsClosed.Should().BeTrue();
    }
}
