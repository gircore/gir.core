using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using GLib;

namespace GObject.Internal;

public static class ObjectWrapper
{
    public static T? WrapNullableHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
    {
        return handle == IntPtr.Zero
            ? null
            : WrapHandle<T>(handle, ownedRef);
    }

    public static T WrapHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
    {
        Debug.Assert(
            condition: typeof(T).IsClass && typeof(T).IsAssignableTo(typeof(GObject.Object)),
            message: "Type 'T' must be a GObject-based class"
        );

        if (handle == IntPtr.Zero)
            throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

        if (ObjectMapper.TryGetObject(handle, out T? obj))
            return obj;

        //In case of classes prefer the type reported by the gobject type system over
        //the expected type as often an API returns a less derived class in it's public
        //API then the actual one.
        Type gtype = GetTypeFromInstance(handle);

        Debug.Assert(
            condition: Functions.TypeName(gtype.Value).ConvertToString() == Functions.TypeNameFromInstance(new TypeInstanceUnownedHandle(handle)).ConvertToString(),
            message: "GType name of instance and class do not match"
        );

        System.Type trueType = TypeDictionary.GetSystemType(gtype);
        ConstructorInfo? ctor = GetObjectConstructor(trueType);

        if (ctor == null)
            throw new Exception($"Type {typeof(T).FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

        return (T) ctor.Invoke(new object[] { handle, ownedRef });
    }

    public static T? WrapNullableInterfaceHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
    {
        return handle == IntPtr.Zero
            ? null
            : WrapInterfaceHandle<T>(handle, ownedRef);
    }

    public static T WrapInterfaceHandle<T>(IntPtr handle, bool ownedRef) where T : class, IHandle
    {
        Debug.Assert(
            condition: typeof(T).IsClass && typeof(T).IsAssignableTo(typeof(GObject.Object)),
            message: "Type 'T' must be a GObject-based class"
        );

        if (handle == IntPtr.Zero)
            throw new NullReferenceException($"Failed to wrap handle as type <{typeof(T).FullName}>. Null handle passed to WrapHandle.");

        if (ObjectMapper.TryGetObject(handle, out T? obj))
            return obj;

        //In case of interfaces prefer the given type over the type reported by the gobject
        //type system as the reported type is probably not part of the public API. Otherwise the
        //class itself would be returned and not an interface.
        ConstructorInfo? ctor = GetObjectConstructor(typeof(T));

        if (ctor == null)
            throw new Exception($"Type {typeof(T).FullName} does not define an IntPtr constructor. This could mean improperly defined bindings");

        return (T) ctor.Invoke(new object[] { handle, ownedRef });
    }

    private static Type GetTypeFromInstance(IntPtr handle)
    {
        TypeInstanceData instance = Marshal.PtrToStructure<TypeInstanceData>(handle);
        TypeClassData klass = Marshal.PtrToStructure<TypeClassData>(instance.GClass);
        var typeid = klass.GType;

        if (typeid == 0)
            throw new Exception("Could not retrieve type from class struct - is the struct valid?");

        return new Type(typeid);
    }

    private static ConstructorInfo? GetObjectConstructor(System.Type type)
    {
        // Create using 'IntPtr' constructor
        ConstructorInfo? ctor = type.GetConstructor(
            System.Reflection.BindingFlags.NonPublic
            | System.Reflection.BindingFlags.Public
            | System.Reflection.BindingFlags.Instance,
            null, new[] { typeof(IntPtr), typeof(bool) }, null
        );
        return ctor;
    }
}
