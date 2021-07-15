using System;
using System.Runtime.InteropServices;

namespace Gdk
{
    public partial class Event
    {
        public static Event __FactoryNew(IntPtr ptr)
        {
            var ev = Marshal.PtrToStructure<Native.EventAny.Struct>(ptr);
            switch (ev.Type)
            {
                case EventType.KeyPress:
                    return EventKey.__FactoryNew(ptr);
                default:
                    throw new NotImplementedException("Event Type not yet supported");
            }
        }
    }
}
