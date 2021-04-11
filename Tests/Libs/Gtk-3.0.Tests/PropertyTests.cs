using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests
{
    [TestClass, TestCategory("IntegrationTest")]
    public class PropertyTests
    {
        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void TestBoolProperty(bool value)
        {
            var window = new Window("TestWindow");
            window.Resizable = value;

            window.Resizable.Should().Be(value);
        }
    }
}
