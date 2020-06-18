using System;

namespace Gir.Core.Gst
{
    public class Element : GObject.Core.GObject
    {        
        internal Element(IntPtr handle) : base(handle, true)
        {
        }

        public Bus GetBus()
        {
            var ret = global::Gst.Element.get_bus(this);
            return GObject.Core.GObject.Convert(ret, (r) => new Bus(r));
        }

        public void SetState(State state) 
            => global::Gst.Element.set_state(this, (global::Gst.State) state);
    }
}