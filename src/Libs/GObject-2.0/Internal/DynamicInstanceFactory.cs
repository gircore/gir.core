using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GObject.Internal;

public static class DynamicInstanceFactory
{
    private static readonly Dictionary<Type, CreateInstance> InstanceFactories = new();

    public static void Register(Type type, CreateInstance handleWrapper)
    {
        InstanceFactories.Add(type, handleWrapper);
    }

    internal static object Create<TFallback>(IntPtr handle, bool ownsHandle) where TFallback : InstanceFactory, GTypeProvider
    {
        var type = GetType(handle);
        var createInstance = GetInstanceFactory<TFallback>(type);
        return createInstance(handle, ownsHandle);
    }

    internal static CreateInstance GetInstanceFactory<TFallback>(Type gtype) where TFallback : InstanceFactory, GTypeProvider
    {
        if (InstanceFactories.TryGetValue(gtype, out CreateInstance? factory))
            return factory;

        var fallbackType = TFallback.GetGType();

        // If the gtype is not found this could mean it is some anonymous subclass
        // which is not public. So we look for the parent type, because this one could be known.
        // If the parent is fundamental (e.g. GObject.Object) this is too unspecific to create.
        var parent = GObject.Functions.TypeParent(gtype);
        if (!Functions.IsFundamental(parent) && Functions.TypeIsA(parent, fallbackType))
            if (InstanceFactories.TryGetValue(parent, out factory))
                return factory;

        // If the gtype is not found this could mean it implements some known interface
        foreach (var iface in GObject.Functions.TypeInterfaces(gtype))
            if (Functions.TypeIsA(iface, fallbackType))
                if (InstanceFactories.TryGetValue(iface, out factory))
                    return factory;

        return TFallback.Create;
    }

    private static unsafe Type GetType(IntPtr handle)
    {
        var gclass = Unsafe.AsRef<TypeInstanceData>((void*) handle).GClass;
        var gtype = Unsafe.AsRef<TypeClassData>((void*) gclass).GType;

        if (gtype == 0)
            throw new Exception("Could not retrieve type from class struct - is the struct valid?");

        return new Type(gtype);
    }
}
