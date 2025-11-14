using System;
using AwesomeAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GirTest.Tests;

[TestClass, TestCategory("BindingTest")]
public class InterfaceSignalTest : Test
{
    [TestMethod]
    public void SupportsSignals()
    {
        var tester = InterfaceSignalTester.New();
        var result = false;

        tester.OnMySignal += (sender, args) =>
        {
            result = true;
        };

        tester.Emit();
        result.Should().BeTrue();
    }

    [TestMethod]
    public void SupportsSignalsViaDescriptor()
    {
        var tester = InterfaceSignalTester.New();
        var result = false;

        InterfaceSignalTester.MySignalSignal.Connect(tester, (sender, args) =>
        {
            result = true;
        });

        tester.Emit();
        result.Should().BeTrue();
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void SupportsNamedSignals(bool valid)
    {
        var tester = InterfaceSignalTester.New();
        var received = false;
        var detail = valid ? "fubar" : "f";

        InterfaceSignalTester.MySignalSignal.Connect(tester, (sender, args) =>
        {
            received = true;
        }, false, detail);

        tester.Emit();
        received.Should().Be(valid);
    }

    [TestMethod]
    public void SupportsConnectingMultipeIdenticalHandlers()
    {
        var emptyQuark = GLib.Functions.QuarkFromString(null);

        var tester = InterfaceSignalTester.New();
        var result = 0;

        tester.OnMySignal += TesterOnOnMySignal;
        tester.OnMySignal += TesterOnOnMySignal;

        void TesterOnOnMySignal(Signaler sender, EventArgs args)
        {
            result++;
        }

        tester.Emit();
        result.Should().Be(2);
        GObject.Functions.SignalHasHandlerPending(tester, InterfaceSignalTester.MySignalSignal.Id, emptyQuark, true).Should().BeTrue();

        tester.OnMySignal -= TesterOnOnMySignal;
        tester.Emit();
        result.Should().Be(3);

        tester.OnMySignal -= TesterOnOnMySignal;
        tester.Emit();
        result.Should().Be(3);
        GObject.Functions.SignalHasHandlerPending(tester, InterfaceSignalTester.MySignalSignal.Id, emptyQuark, true).Should().BeFalse();
    }

    [TestMethod]
    public void CanCollectInstanceWithConnectedSignal()
    {
        var reference = new System.WeakReference(null);

        CollectAfter(() =>
        {
            var tester = InterfaceSignalTester.New();
            tester.OnMySignal += (sender, args) => { };
            reference.Target = tester;
        });

        reference.IsAlive.Should().BeFalse();
    }
}
