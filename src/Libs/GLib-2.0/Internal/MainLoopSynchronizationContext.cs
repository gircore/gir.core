using System;
using System.Threading;

namespace GLib.Internal;

public sealed class MainLoopSynchronizationContext : SynchronizationContext
{
    public override SynchronizationContext CreateCopy()
        => new MainLoopSynchronizationContext();

    public override void Post(SendOrPostCallback d, object? state)
        => ScheduleAction(() => d(state));

    public override void Send(SendOrPostCallback d, object? state)
    {
        using var resetEvent = new ManualResetEventSlim(false);

        ScheduleAction(() =>
        {
            d(state);
            resetEvent.Set();
        });

        resetEvent.Wait();
    }

    private static void ScheduleAction(Action action)
    {
        var proxy = new SourceFuncNotifiedHandler(() =>
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                UnhandledException.Raise(ex);
            }

            return false;
        });

        Functions.IdleAdd(Constants.PRIORITY_DEFAULT_IDLE, proxy.NativeCallback, IntPtr.Zero, proxy.DestroyNotify);
    }
}
