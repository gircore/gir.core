using System;
using System.Threading;

namespace GLib.Internal;

public sealed class MainLoopSynchronizationContext : SynchronizationContext
{
    public override SynchronizationContext CreateCopy()
        => new MainLoopSynchronizationContext();

    public override void Post(SendOrPostCallback d, object? state)
    {
        var proxy = new SourceFuncNotifiedHandler(() =>
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                UnhandledException.Raise(ex);
            }

            return false;
        });

        Functions.IdleAdd(Constants.PRIORITY_DEFAULT_IDLE, proxy.NativeCallback, IntPtr.Zero, proxy.DestroyNotify);
    }

    public override void Send(SendOrPostCallback d, object? state)
    {
        var proxy = new SourceFuncAsyncHandler(() =>
        {
            try
            {
                d(state);
            }
            catch (Exception ex)
            {
                UnhandledException.Raise(ex);
            }

            return false;
        });

        MainContext.Invoke(MainContextUnownedHandle.NullHandle, proxy.NativeCallback, IntPtr.Zero);
    }
}
