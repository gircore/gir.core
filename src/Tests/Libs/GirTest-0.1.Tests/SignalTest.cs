using System.Diagnostics;
using FluentAssertions;
using GObject;
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
    public void TestForRaceConditionsIfMemoryIsFeed()
    {
        var tester = SignalTester.New();
        tester.OnMyObjSignal += TesterOnOnMyObjSignal;

        void TesterOnOnMyObjSignal(SignalTester sender, SignalTester.MyObjSignalSignalArgs args)
        {
            args.Object.Should().NotBeNull();
            System.GC.Collect();
        }

        for (var a = 0; a < 1000; a++)
        {
            tester.EmitMyObjSignal();
        }

        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();

        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
    }
}
