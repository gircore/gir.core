using System;

namespace GObject;

public interface InstanceFactory
{
    static abstract object Create(IntPtr handle, bool ownsHandle);
}
