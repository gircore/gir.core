using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests
{
    [TestClass, TestCategory("SystemTest")]
    public class SignalTest : Test
    {
        [TestMethod]
        public void TestOnNotifySignal()
        {
            var senderOk = false;
            var parameterNameOk = false;

            var window = new Window();
            window.OnNotify += (sender, args) =>
            {
                senderOk = sender == window;
                parameterNameOk = args.Pspec.GetName() == Window.TitlePropertyDefinition.Name;
            };

            window.Title = "Title";

            senderOk.Should().BeTrue();
            parameterNameOk.Should().BeTrue();
        }
    }
}
