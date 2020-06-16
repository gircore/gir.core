using System;
using GObject.Core;

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
            if(TryGetObject(ret, out Bus obj))
                return obj;
            else
                return new Bus(ret);
        }

        public void SetState(State state) 
            => global::Gst.Element.set_state(this, (global::Gst.State) state);
    }
}