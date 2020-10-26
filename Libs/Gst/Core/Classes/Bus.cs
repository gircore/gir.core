using System;

namespace Gst
{
    public class Bus : GObject.InitiallyUnowned
    {
        protected internal Bus(IntPtr handle) : base(handle) { }

        public void WaitForEndOrError()
            => TimedPopFiltered(Sys.Constants.CLOCK_TIME_NONE);
            
        public void TimedPopFiltered(ulong timeout)
            => Sys.Bus.timed_pop_filtered(Handle, timeout, (Sys.MessageType) (MessageType.EOS | MessageType.Error));
    }
}