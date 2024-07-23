using System;
using System.Linq;
using System.Reflection;

namespace GObject.Internal;

public class BoxedWrapper
{
    public static object WrapHandle(IntPtr handle, bool ownsHandle, Type gtype)
    {
        //TODO: REMOVE BOXED WRAPPER
        return InstanceFactory.Create(handle, ownsHandle);
    }
}
