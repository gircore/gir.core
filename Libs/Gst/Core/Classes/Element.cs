using System;

namespace Gir.Core.Gst
{
    public class Element : GObject.Core.GObject
    {
        internal Element(IntPtr handle) : base(handle, true)
        {
        }
    }
}