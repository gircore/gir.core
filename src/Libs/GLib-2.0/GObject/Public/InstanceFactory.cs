using System;

namespace GObject;

public interface InstanceFactory
{
#if NET7_0_OR_GREATER
    static abstract object Create(IntPtr handle, bool ownsHandle);
#endif
}
