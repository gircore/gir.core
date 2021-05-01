using System;
using System.Runtime.InteropServices;

namespace GLib
{
    public static class Timeout
    {
        public static uint AddFull(int priority, uint interval, SourceFunc function)
            => AddFull(priority, interval, function, null);

        public static uint AddFull(int priority, uint interval, SourceFunc function, Action? destroyNotify)
        {
            // This uses scope=notified (see timeout_add_full's gir data)
            //var handler = new SourceFuncNotifiedHandler(function);
            //handler.OnDestroyNotify += destroyNotify;

            return Native.Functions.TimeoutAddFull(
                priority: priority,
                interval: interval,
                function: default!, //TODO
                data: IntPtr.Zero,
                notify: default!); //handler.DestroyNotify);
        }
    }
}
