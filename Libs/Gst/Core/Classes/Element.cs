using System;

namespace Gst
{
    public class Element : GObject.Object
    {        
        internal Element(IntPtr handle) : base(handle, true) { }

        public Bus GetBus()
        {
            var ret = Sys.Element.get_bus(this);
            return Convert(ret, (r) => new Bus(r));
        }

        public void SetState(State state) 
            => Sys.Element.set_state(this, (Sys.State) state);
    }
}