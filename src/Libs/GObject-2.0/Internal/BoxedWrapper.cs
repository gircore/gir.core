using System;

namespace GObject.Internal;

public class BoxedWrapper
{
    public static object WrapHandle(IntPtr handle, bool ownsHandle, Type gtype)
    {
        if (handle == IntPtr.Zero)
            throw new NullReferenceException("Failed to wrap boxed handle as a NULL handle was given.");

        var createInstance = DynamicInstanceFactory.GetInstanceFactory(gtype);
        
       return createInstance(handle, ownsHandle);
    }
}
