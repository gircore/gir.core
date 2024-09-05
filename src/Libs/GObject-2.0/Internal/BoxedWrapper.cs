using System;

namespace GObject.Internal;

public class BoxedWrapper
{
    public static object WrapHandle(IntPtr handle, bool ownsHandle, Type gtype)
    {
        if (handle == IntPtr.Zero)
            throw new NullReferenceException("Failed to wrap boxed handle as a NULL handle was given.");

        //Using GObject.Object as a fallback type is not strictly correct as this handler is used for boxed
        //records. As boxed records are known through the type system the fallback is never actually used.
        var createInstance = DynamicInstanceFactory.GetInstanceFactory<GObject.Object>(gtype);

        return createInstance(handle, ownsHandle);
    }
}
