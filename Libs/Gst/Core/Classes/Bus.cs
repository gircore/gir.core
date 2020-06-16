using System;

namespace Gir.Core.Gst
{
    public class Bus : GObject.Core.GObject
    {
        protected internal Bus(IntPtr handle) : base(handle, true)
        {
        }

        public void TimedPopFiltered(ulong timeout)
            => global::Gst.Bus.timed_pop_filtered(this, timeout, (global::Gst.MessageType) (MessageType.EOS | MessageType.Error));
    }
}