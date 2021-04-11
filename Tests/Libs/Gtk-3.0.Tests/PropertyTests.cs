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

        [DataTestMethod]
        [DataRow("NewTitle")]
        public void TestStringProperty(string str)
        {
            var window = new Window("TestWindow");
            window.Title = str;

            window.Title.Should().Be(str);
        }
    }
}
