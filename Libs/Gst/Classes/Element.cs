using System;

namespace Gst
{
    public partial class Element
    {
        public Bus GetBus()
        {
            IntPtr ret = Native.get_bus(Handle);
            
            if (GetObject<Bus>(ret, out var obj))
                return obj;

            if(ret == IntPtr.Zero)
                throw new Exception("Could not convert pointer to bus");
            
            return new Bus(ret);
        }

        public void SetState(State state) 
            => Native.set_state(Handle, state);
    }
}
