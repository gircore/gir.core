using System.Diagnostics;

namespace GObject.Internal;

/// <summary>
/// Registers a custom subclass with the GObject type system.
/// </summary>
public static class SubclassRegistrar
{
    public static Type Register<TSubclass, TParent>()
        where TSubclass : InstanceFactory
        where TParent : GTypeProvider
    {
        var newType = RegisterNewGType<TSubclass, TParent>();
        DynamicInstanceFactory.Register(newType, TSubclass.Create);

        return newType;
    }

    private static Type RegisterNewGType<TSubclass, TParent>()
        where TParent : GTypeProvider
    {
        var parentType = TParent.GetGType();
        var parentTypeInfo = TypeQueryOwnedHandle.Create();
        Functions.TypeQuery(parentType, parentTypeInfo);

        if (parentTypeInfo.GetType() == 0)
            throw new TypeRegistrationException("Could not query parent type");

        Debug.WriteLine($"Registering new type {typeof(TSubclass).FullName} with parent {typeof(TParent).FullName}");

        // Create TypeInfo
        //TODO: Callbacks for "ClassInit" and "InstanceInit" are disabled because if multiple instances
        //of the same type are created, the typeInfo object can get garbagec collected in the mean time
        //and with it the instances of "DoClassInit" and "DoInstanceInit". If the callback occurs the
        //runtime can't do the call anymore and crashes with:
        //A callback was made on a garbage collected delegate of type 'GObject-2.0!GObject.Internal.InstanceInitFunc::Invoke'
        //Fix this by caching the garbage collected instances somehow
        var handle = TypeInfoOwnedHandle.Create();
        handle.SetClassSize((ushort) parentTypeInfo.GetClassSize());
        handle.SetInstanceSize((ushort) parentTypeInfo.GetInstanceSize());
        //handle.SetClassInit();
        //handle.SetInstanceInit();

        var qualifiedName = QualifyName(typeof(TSubclass));
        var typeid = Functions.TypeRegisterStatic(parentType,
            GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(qualifiedName), handle, 0);

        if (typeid == 0)
            throw new TypeRegistrationException("Type Registration Failed!");

        return new Type(typeid);
    }

    private static string QualifyName(System.Type type)
        => type.ToString()
            .Replace(".", string.Empty)
            .Replace("+", string.Empty)
            .Replace("`", string.Empty)
            .Replace("[", "_")
            .Replace("]", string.Empty)
            .Replace(" ", string.Empty)
            .Replace(",", "_");

    /* TODO: Enable if init functions are supported again
    // Default Handler for class initialisation.
    private static void DoClassInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass type class initialised!");
    }
    // Default Handler for instance initialisation.
    private static void DoInstanceInit(IntPtr gClass, IntPtr classData)
    {
        Console.WriteLine("Subclass instance initialised!");
    }
    */
}
