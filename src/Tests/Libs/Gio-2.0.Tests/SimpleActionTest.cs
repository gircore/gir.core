using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gio.Tests;

[TestClass, TestCategory("SystemTest")]
public class SimpleActionTest : Test
{
    [TestMethod]
    public void TestActivate()
    {
        var action = SimpleAction.NewStateful("myname", GLib.VariantType.String, GLib.Variant.NewString("foo"));

        var result = action.GetState().GetString(out _);
        result.Should().Be("foo");

        action.OnActivate += (_, args) =>
        {
            result = args.Parameter!.GetString(out var _);
        };

        action.Activate(GLib.Variant.NewString("bar"));

        result.Should().Be("bar");
    }
}
