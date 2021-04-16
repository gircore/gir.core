using System;
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

        [TestMethod]
        public void TestStringArray()
        {
            throw new NotImplementedException();
            /*string[] bla = new[] {"hallo", "123"};
            var aboutDialog = new AboutDialog();
            aboutDialog.Artists = bla;

            aboutDialog.Artists.First().Should().Be("hallo");*/
        }
    }
}
