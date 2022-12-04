using System;
using System.Linq;
using System.Reflection;

namespace GObject.Internal;

public class BoxedWrapper
{
    public static object WrapHandle(IntPtr handle, bool ownsHandle, Type gtype)
    {
        System.Type trueType = TypeDictionary.GetSystemType(gtype);

        if (handle == IntPtr.Zero)
            throw new NullReferenceException($"Failed to wrap handle as type <{trueType}>. Null handle passed to WrapHandle.");

        // Get constructor for the true type
        var ctr = GetBoxedConstructor(trueType);

        if (ctr is null)
            throw new Exception($"Type {trueType} does not define an IntPtr constructor. This could mean improperly defined bindings");

        var result = ctr.Invoke(new object[] { handle, ownsHandle });

        if (result == null)
            throw new Exception($"Type {trueType}'s factory method returned a null object. This could mean improperly defined bindings");

        return result;
    }

    private static ConstructorInfo? GetBoxedConstructor(System.Type type)
    {
        // Create using 'IntPtr, ownsHandle' constructor
        ConstructorInfo? ctor = type.GetConstructor(
            System.Reflection.BindingFlags.NonPublic
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance,
            null, new[] { typeof(IntPtr), typeof(bool) }, null
        );
        return ctor;
    }
}
