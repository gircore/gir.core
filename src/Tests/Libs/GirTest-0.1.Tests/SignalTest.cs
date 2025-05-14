using Combinatorial.MSTest;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class SignalTest : Test
{
    [TestMethod]
    public void SupportsSignals()
    {
        var tester = SignalTester.New();
        var result = false;

        tester.OnMySignal += (sender, args) =>
        {
            result = true;
        };

        tester.EmitMySignalFubar();
        result.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsSignalsViaDescriptor()
    {
        var tester = SignalTester.New();
        var result = false;

        SignalTester.MySignalSignal.Connect(tester, (sender, args) =>
        {
            result = true;
        });

        tester.EmitMySignalFubar();
        result.Should().BeTrue();
    }

    [DataTestMethod]
    [CombinatorialData]
    public void SupportsNamedSignals(bool valid)
    {
        var tester = SignalTester.New();
        var received = false;
        var detail = valid ? "fubar" : "f";

        SignalTester.MySignalSignal.Connect(tester, (sender, args) =>
        {
            received = true;
        }, false, detail);

        tester.EmitMySignalFubar();
        received.Should().Be(valid);
    }
}
