using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class FunctionTest : Test
{
    [TestMethod]
    public void CanSetApplicationName()
    {
        // Simple test of global functions.
        GLib.Functions.GetApplicationName().Should().Be("");
        GLib.Functions.SetApplicationName("foo");
        GLib.Functions.GetApplicationName().Should().Be("foo");
    }
}

