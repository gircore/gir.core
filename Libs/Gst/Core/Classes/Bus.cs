using System;

namespace Gst
{
    public class Bus : GObject.Object
    {
        protected internal Bus(IntPtr handle) : base(handle, true) { }

        public void WaitForEndOrError()
            => TimedPopFiltered(Sys.Constants.CLOCK_TIME_NONE);
            
        public void TimedPopFiltered(ulong timeout)
            => Sys.Bus.timed_pop_filtered(this, timeout, (Sys.MessageType) (MessageType.EOS | MessageType.Error));
    }
}