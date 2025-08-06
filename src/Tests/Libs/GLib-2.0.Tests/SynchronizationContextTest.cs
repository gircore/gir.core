using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AwesomeAssertions;
using GLib.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("UnitTest")]
public class SynchronizationContextTest : Test
{
    [TestMethod]
    public void AsyncMethodIsExecutedOnMainLoopThread()
    {
        var mainLoop = MainLoop.New(null, false);
        var context = mainLoop.GetContext();
        var source = Functions.TimeoutSourceNew(1);
        source.Attach(context);

        int? resumeThread = null;
        source.SetCallback(() =>
        {
            AsyncMethod(() =>
            {
                resumeThread = System.Threading.Thread.CurrentThread.ManagedThreadId;
                mainLoop.Quit();
            });
            return false;
        });

        mainLoop.RunWithSynchronizationContext();

        resumeThread.Should().Be(System.Threading.Thread.CurrentThread.ManagedThreadId);
    }

    [TestMethod]
    public void ExceptionInAsyncMethodCanBeHandledViaUnhandledExceptionHandler()
    {
        var mainLoop = MainLoop.New(null, false);
        var context = mainLoop.GetContext();
        var source = Functions.TimeoutSourceNew(1);
        source.Attach(context);
        source.SetCallback(() =>
        {
            AsyncMethod(() =>
            {
                throw new Exception();
            });
            return false;
        });

        var exceptionCaught = false;
        UnhandledException.SetHandler(e =>
        {
            exceptionCaught = true;
            mainLoop.Quit();
        });

        mainLoop.RunWithSynchronizationContext();

        exceptionCaught.Should().BeTrue();
    }

    private static async void AsyncMethod(Action finish)
    {
        await Task.Delay(1);
        finish();
    }

    [TestMethod]
    public void SendIsDirectlyExecutedOnMainThread()
    {
        int? thread = null;

        var c = new MainLoopSynchronizationContext();
        c.Send((o) =>
        {
            thread = System.Threading.Thread.CurrentThread.ManagedThreadId;
        }, null);

        thread.Should().Be(System.Threading.Thread.CurrentThread.ManagedThreadId);
    }

    [TestMethod]
    public void SendIsDispatchedToMainThread()
    {
        int mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        int newThreadId = 0;
        int sendThreadId = 0;

        var context = Functions.MainContextDefault();
        var mainLoop = MainLoop.New(context, false);
        var source = Functions.TimeoutSourceNew(5);
        source.Attach(context);
        source.SetCallback(() =>
        {
            var t = new System.Threading.Thread(() =>
            {
                newThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;

                var c = new MainLoopSynchronizationContext();
                c.Send((o) =>
                {
                    sendThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
                    mainLoop.Quit();
                }, null);
            });
            t.Start();
            t.Join();
            return false;
        });

        mainLoop.RunWithSynchronizationContext();

        newThreadId.Should().NotBe(0);
        sendThreadId.Should().NotBe(0);
        mainThreadId.Should().NotBe(newThreadId);
        mainThreadId.Should().Be(sendThreadId);
    }

    [TestMethod]
    public void SendCanCatchUnhandledException()
    {
        var exceptionCaught = false;

        var context = Functions.MainContextDefault();
        var mainLoop = MainLoop.New(context, false);
        var source = Functions.TimeoutSourceNew(5);
        source.Attach(context);
        source.SetCallback(() =>
        {
            var t = new System.Threading.Thread(() =>
            {
                var c = new MainLoopSynchronizationContext();
                c.Send((o) => throw new Exception("Test"), null);
            });
            t.Start();
            t.Join();
            return false;
        });

        UnhandledException.SetHandler(e =>
        {
            exceptionCaught = true;
            mainLoop.Quit();
        });

        mainLoop.RunWithSynchronizationContext();

        exceptionCaught.Should().BeTrue();
    }
}
