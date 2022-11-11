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
            var window = new Window();
            window.Resizable = value;

            window.Resizable.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow("NewTitle")]
        [DataRow("Some Text With Unicode ☀🌙🌧")]
        public void TestStringProperty(string str)
        {
            var window = new Window();
            window.Title = str;

            window.Title.Should().Be(str);
        }

        [TestMethod]
        public void TestNullStringProperty()
        {
            var title = "Title";
            var window = new Window();
            window.Title = title;
            window.Title.Should().Be(title);
            window.Title = null;
            window.Title.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(500)]
        public void TestIntegerProperty(int value)
        {
            var window = new Window();
            window.DefaultWidth = value;

            window.DefaultWidth.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow(7u)]
        public void TestUnsignedIntegerProperty(uint value)
        {
            var spinButton = new SpinButton();
            spinButton.Digits = value;

            spinButton.Digits.Should().Be(value);
        }

        [DataTestMethod]
        [DataRow(0.5)]
        [DataRow(0.1)]
        [DataRow(0.9)]
        public void TestDoubleProperty(double value)
        {
            var window = new Window();
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
        [DataRow(License.MitX11)]
        [DataRow(License.Agpl30)]
        public void TestEnum(License windowPosition)
        {
            var aboutDialog = new AboutDialog();
            aboutDialog.LicenseType = windowPosition;

            aboutDialog.LicenseType.Should().Be(windowPosition);
        }

        [TestMethod]
        public void TestObject()
        {
            var pixbuf = GdkPixbuf.Pixbuf.NewFromFile("test.bmp");
            var texture = Gdk.Texture.NewForPixbuf(pixbuf);
            var dialog = new AboutDialog();
            dialog.Logo.Should().BeNull();
            dialog.Logo = texture;
            dialog.Logo.Should().Be(texture);
            dialog.Logo = null!;
            dialog.Logo.Should().BeNull();
        }
    }
}
