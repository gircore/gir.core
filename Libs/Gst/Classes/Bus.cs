using System;
using GLib;

namespace Gst
{
    public partial class Bus
    {
        //TODO: This method is a shortcut for the user and should probably be part of the toolkit layer
        public void WaitForEndOrError()
            => TimedPopFiltered(Constants.CLOCK_TIME_NONE);

        //TODO: This method is a shortcut for the user and should probably be part of the toolkit layer
        public void TimedPopFiltered(ulong timeout)
            => Native.timed_pop_filtered(Handle, timeout, (MessageType.Eos | MessageType.Error));

        public uint AddWatchFull(int priority, BusFunc func)
            => AddWatchFull(priority, func, null);

        public uint AddWatchFull(int priority, BusFunc func, DestroyNotify? notify)
            => Native.add_watch_full(Handle, priority, func, IntPtr.Zero, notify!);
    }
}
