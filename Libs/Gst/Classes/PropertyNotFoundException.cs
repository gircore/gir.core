using System;

namespace Gst
{
    [Obsolete]
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException() { }
        public PropertyNotFoundException(Exception inner) : base(String.Empty, inner) { }
    }
}
