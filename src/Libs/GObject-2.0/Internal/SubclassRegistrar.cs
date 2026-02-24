using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace GObject.Internal;

/// <summary>
/// Registers a custom subclass with the GObject type system.
/// </summary>
public static unsafe class SubclassRegistrar
{
    [DllImport(ImportResolver.Library, EntryPoint = "g_type_register_static_simple")]
    private static extern nuint TypeRegisterStaticSimple(Type parentType, GLib.Internal.NonNullableUtf8StringHandle typeName, uint classSize, delegate* unmanaged<IntPtr, IntPtr, void> classInit, uint instanceSize, delegate* unmanaged<IntPtr, IntPtr, void> instanceInit, TypeFlags flags);

    public static Type Register<TSubclass, TParent>(delegate* unmanaged<IntPtr, IntPtr, void> classInit, delegate* unmanaged<IntPtr, IntPtr, void> instanceInit)
        where TSubclass : InstanceFactory
        where TParent : GTypeProvider
    {
        var newType = RegisterNewGType<TSubclass, TParent>(classInit, instanceInit);
        DynamicInstanceFactory.Register(newType, TSubclass.Create);

        return newType;
    }

    private static Type RegisterNewGType<TSubclass, TParent>(delegate* unmanaged<IntPtr, IntPtr, void> classInit, delegate* unmanaged<IntPtr, IntPtr, void> instanceInit)
        where TParent : GTypeProvider
    {
        var parentType = TParent.GetGType();
        var parentTypeInfo = TypeQueryOwnedHandle.Create();
        Functions.TypeQuery(parentType, parentTypeInfo);

        if (parentTypeInfo.GetType() == 0)
            throw new TypeRegistrationException("Could not query parent type");

        Debug.WriteLine($"Registering new type {typeof(TSubclass).FullName} with parent {typeof(TParent).FullName}");

        var qualifiedName = QualifyName(typeof(TSubclass));
        var typeName = GLib.Internal.NonNullableUtf8StringOwnedHandle.Create(qualifiedName);
        var classSize = (ushort) parentTypeInfo.GetClassSize();
        var instanceSize = (ushort) parentTypeInfo.GetInstanceSize();

        var typeid = TypeRegisterStaticSimple(
            parentType: parentType,
            typeName: typeName,
            classSize: classSize,
            classInit: classInit,
            instanceSize: instanceSize,
            instanceInit: instanceInit,
            flags: TypeFlags.None
        );

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
}

