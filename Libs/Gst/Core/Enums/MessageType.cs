using System;

namespace Gir.Core.Gst
{
    [Flags]
    public enum MessageType
    {
        EOS = 1,
        Error = 2
    }
}