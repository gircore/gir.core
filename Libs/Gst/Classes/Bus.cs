namespace Gst
{
    public partial class Bus
    {
        //TODO: This method is a shortcut for the user and should probably be part of the toolkit layer
        public void WaitForEndOrError()
            => TimedPopFiltered(Constant.CLOCK_TIME_NONE);

        //TODO: This method is a shortcut for the user and should probably be part of the toolkit layer
        public void TimedPopFiltered(ulong timeout)
            => Native.timed_pop_filtered(Handle, timeout, (MessageType.Eos | MessageType.Error));
    }
}
