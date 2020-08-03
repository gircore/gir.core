using System;

namespace Gst
{
    [Flags]
    public enum MessageType
    {
        EOS = 1,
        Error = 2
    }
}