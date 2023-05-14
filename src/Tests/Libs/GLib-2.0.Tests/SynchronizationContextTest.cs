using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GLib.Tests;

[TestClass, TestCategory("IntegrationTest")]
public class SynchronizationContextTest : Test
{
    [TestMethod]
    public void AsyncMethodIsExecutedOnMainLoopThread()
    {
        var mainLoop = new MainLoop();
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
        var mainLoop = new MainLoop();
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
}
