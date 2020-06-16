using System;
using GObject.Core;

namespace Gir.Core.Gst
{
    public class Element : GObject.Core.GObject
    {
        public Property<Bus> Bus { get; }
        
        internal Element(IntPtr handle) : base(handle, true)
        {
            Bus = Property<Bus>("bus",
                get: GetObject<Bus>,
                set: Set
            );
        }

        public void SetState(State state) 
            => global::Gst.Element.set_state(this, (global::Gst.State) state);
    }
}