using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GObject.Internal;

public interface Constructable
{
    public static abstract object Create(IntPtr handle, bool ownedRef);
}

/// <summary>
/// Creates new instances of classes and records
/// </summary>
internal class InstanceFactory
{
    private static readonly Dictionary<Type, Func<IntPtr, bool, object>> Factories = new();
    
    public static object Create(IntPtr handle, bool ownedRef)
    {
        var gtype = GetTypeFromInstance(handle);
        
        Debug.Assert(
            condition: Functions.TypeName(gtype.Value).ConvertToString() == Functions.TypeNameFromInstance(new TypeInstanceUnownedHandle(handle)).ConvertToString(),
            message: "GType name of instance and class do not match"
        );

        var factory = GetFactory(gtype);
        
        return factory(handle, ownedRef);
    }

    public static void AddFactoryForType(Type type, Func<IntPtr, bool, object> factory)
    {
        Factories[type] = factory;
    }
    
    private static Func<IntPtr, bool, object> GetFactory(Type gtype)
    {
        if (Factories.TryGetValue(gtype, out var factory))
            return factory;

        do
        {
            var parentType = new Type(Functions.TypeParent(gtype));
            if (parentType.Value is (nuint) BasicType.Invalid or (nuint) BasicType.None)
                throw new Exception("Could not retrieve parent type - is the typeid valid?");

            if (!Factories.TryGetValue(gtype, out var parentFactory))
                continue;

            //Store parent factory for later use
            AddFactoryForType(gtype, parentFactory);

        } while(true);
    }
    
    private static unsafe Type GetTypeFromInstance(IntPtr handle)
    {
        var gclass = Unsafe.AsRef<TypeInstanceData>((void*) handle).GClass;
        var gtype = Unsafe.AsRef<TypeClassData>((void*) gclass).GType;

        if (gtype == 0)
            throw new Exception("Could not retrieve type from class struct - is the struct valid?");

        return new Type(gtype);
    }
}
