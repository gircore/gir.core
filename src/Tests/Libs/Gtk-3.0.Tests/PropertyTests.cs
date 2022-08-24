using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests
{
    [TestClass, TestCategory("SystemTest")]
    public class PropertyTests : Test
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
        [DataRow("Some Text With Unicode ☀🌙🌧")]
        [DataRow(null)]
        public void TestStringProperty(string str)
        {
            var window = new Window("TestWindow");
            window.Title = str;

            window.Title.Should().Be(str);
        }

        [DataTestMethod]
        [DataRow(500)]
        public void TestIntegerProperty(int value)
        {
            var window = new Window("TestWindow");
            window.DefaultWidth = value;

            window.DefaultWidth.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow(7u)]
        public void TestUnsignedIntegerProperty(uint value)
        {
            var window = new Window("TestWindow");
            window.BorderWidth = value;

            window.BorderWidth.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow(0.5)]
        [DataRow(0.1)]
        [DataRow(0.9)]
        public void TestDoubleProperty(double value)
        {
            var window = new Window("TestWindow");
            window.Opacity = value;

            //TODO: It lookls like double values are very unprecise?
            Math.Round(window.Opacity, 2).Should().Be(value);
        }

        [DataTestMethod]
        [DataRow("abc", "def")]
        [DataRow("öö", "ß")]
        public void TestStringArray(string value1, string value2)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.Artists = new[] { value1, value2 };

            aboutDialog.Artists[0].Should().Be(value1);
            aboutDialog.Artists[1].Should().Be(value2);
        }

        [DataTestMethod]
        [DataRow(WindowPosition.Center)]
        [DataRow(WindowPosition.Mouse)]
        public void TestEnum(WindowPosition windowPosition)
        {
            var window = new Window("TestWindow");
            window.WindowPosition = windowPosition;

            window.WindowPosition.Should().Be(windowPosition);
        }

        [TestMethod]
        public void TestObject()
        {
            var pixbuf = GdkPixbuf.Pixbuf.NewFromFile("test.bmp");
            var aboutDialog = new AboutDialog();

            aboutDialog.Logo.Should().BeNull();
            aboutDialog.Logo = pixbuf;
            aboutDialog.Logo.Should().Be(pixbuf);
        }
    }
}
