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
        {
            Native.Bus.Instance.Methods.TimedPopFiltered(Handle, timeout, (MessageType.Eos | MessageType.Error));
        }

        public uint AddWatchFull(int priority, BusFunc func)
            => AddWatchFull(priority, func, null);

        public uint AddWatchFull(int priority, BusFunc func, DestroyNotify? notify)
            => throw new NotImplementedException(); //TODO Native.add_watch_full(Handle, priority, func, IntPtr.Zero, notify!);
    }
}
