namespace Gst
{
    public partial class Bus
    {
        public void WaitForEndOrError()
            => TimedPopFiltered(Constant.CLOCK_TIME_NONE);
            
        public void TimedPopFiltered(ulong timeout)
            => Native.timed_pop_filtered(Handle, timeout, (MessageType.Eos | MessageType.Error));
    }
}
