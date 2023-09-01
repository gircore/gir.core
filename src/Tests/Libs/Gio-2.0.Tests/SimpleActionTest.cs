using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gio.Tests;

[TestClass, TestCategory("SystemTest")]
public class SimpleActionTest : Test
{
    [TestMethod]
    public void TestActivate()
    {
        var action = SimpleAction.NewStateful("myname", GLib.VariantType.String, GLib.Variant.Create("foo"));

        var result = action.GetState().GetString();
        result.Should().Be("foo");

        action.OnActivate += (_, args) =>
        {
            result = args.Parameter!.GetString();
        };

        action.Activate(GLib.Variant.Create("bar"));

        result.Should().Be("bar");
    }
}
