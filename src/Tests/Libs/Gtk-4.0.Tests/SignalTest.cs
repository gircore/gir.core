using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

[TestClass, TestCategory("SystemTest")]
public class SignalTest : Test
{
    [TestMethod]
    public void TestOnNotifySignal()
    {
        var senderOk = false;
        var parameterNameOk = false;

        var window = Window.New();
        window.OnNotify += (sender, args) =>
        {
            senderOk = sender == window;
            parameterNameOk = args.Pspec.GetName() == Window.TitlePropertyDefinition.UnmanagedName;
        };

        window.Title = "Title";

        senderOk.Should().BeTrue();
        parameterNameOk.Should().BeTrue();
    }

    [TestMethod]
    public void CallbackAreInstanceSpecific()
    {
        var notify1Called = false;
        var notify2Called = false;

        var window1 = Window.New();
        window1.OnNotify += (sender, args) => notify1Called = true;

        var window2 = Window.New();
        window2.OnNotify += (sender, args) => notify2Called = true;

        notify1Called.Should().BeFalse();
        notify2Called.Should().BeFalse();

        window2.Title = "Test";

        notify1Called.Should().BeFalse();
        notify2Called.Should().BeTrue();

        window1.Title = "Test";

        notify1Called.Should().BeTrue();
        notify2Called.Should().BeTrue();
    }
}
