using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gtk.Tests;

[TestClass, TestCategory("SystemTest")]
public class PropertyTests : Test
{
    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void TestBoolProperty(bool value)
    {
        var window = new Window();
        window.Resizable = value;

        window.Resizable.Should().Be(value);
    }

    [TestMethod]
    [DataRow(500)]
    public void TestIntegerProperty(int value)
    {
        var window = new Window();
        window.DefaultWidth = value;

        window.DefaultWidth.Should().Be(value);
    }

    [TestMethod]
    [DataRow(7u)]
    public void TestUnsignedIntegerProperty(uint value)
    {
        var spinButton = new SpinButton();
        spinButton.Digits = value;

        spinButton.Digits.Should().Be(value);
    }

    [TestMethod]
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

    [TestMethod]
    [DataRow("abc", "def")]
    [DataRow("öö", "ß")]
    public void TestStringArray(string value1, string value2)
    {
        var aboutDialog = new AboutDialog();
        aboutDialog.Artists = new[] { value1, value2 };

        aboutDialog.Artists[0].Should().Be(value1);
        aboutDialog.Artists[1].Should().Be(value2);
    }

    [TestMethod]
    [DataRow(License.MitX11)]
    [DataRow(License.Agpl30)]
    public void TestEnum(License windowPosition)
    {
        var aboutDialog = new AboutDialog();
        aboutDialog.LicenseType = windowPosition;

        aboutDialog.LicenseType.Should().Be(windowPosition);
    }
}
