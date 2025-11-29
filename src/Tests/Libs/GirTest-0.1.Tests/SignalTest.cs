using System;
using System.Diagnostics;
using AwesomeAssertions;
using GLib;
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

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
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

    [TestMethod]
    public void SupportsConnectingMultipeIdenticalHandlers()
    {
        var emptyQuark = GLib.Functions.QuarkFromString(null);

        var tester = SignalTester.New();
        var result = 0;

        tester.OnMySignal += TesterOnOnMySignal;
        tester.OnMySignal += TesterOnOnMySignal;

        void TesterOnOnMySignal(SignalTester sender, EventArgs args)
        {
            result++;
        }

        tester.EmitMySignalFubar();
        result.Should().Be(2);
        GObject.Functions.SignalHasHandlerPending(tester, SignalTester.MySignalSignal.Id, emptyQuark, true).Should().BeTrue();

        tester.OnMySignal -= TesterOnOnMySignal;
        tester.EmitMySignalFubar();
        result.Should().Be(3);

        tester.OnMySignal -= TesterOnOnMySignal;
        tester.EmitMySignalFubar();
        result.Should().Be(3);
        GObject.Functions.SignalHasHandlerPending(tester, SignalTester.MySignalSignal.Id, emptyQuark, true).Should().BeFalse();
    }

    [TestMethod]
    public void CanCollectInstanceWithConnectedSignal()
    {
        var reference = new System.WeakReference(null);

        CollectAfter(() =>
        {
            var tester = SignalTester.New();
            tester.OnMySignal += (sender, args) => { };
            reference.Target = tester;
        });

        reference.IsAlive.Should().BeFalse();
    }

    [TestMethod]
    public void SupportsSignalsWithGLibBasedParameters()
    {
        var tester = SignalTester.New();
        var result = false;

        tester.OnGbytesSignal += (sender, args) =>
        {
            var bytes = args.Object;
            bytes.Should().NotBeNull();
            result = true;
        };

        tester.EmitGbytesSignal();
        result.Should().BeTrue();
    }
}
