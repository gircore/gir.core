namespace Generator.Model;

internal static partial class Record
{
    public static bool IsForeignTyped(GirModel.Record record)
    {
        return record is { Foreign: true, TypeFunction.CIdentifier: not null };
    }

    public static bool IsForeignUntyped(GirModel.Record record)
    {
        return record is { Foreign: true, TypeFunction: null or { CIdentifier: "intern" } };
    }

    public static bool IsOpaqueTyped(GirModel.Record record)
    {
        //Even if there is a TypeFunction it does not mean that it actually is
        //a typed / boxed record. There is a magic keyword "intern" which means this
        //record is actually fundamental and does not have a type function.
        return record is { Foreign: false, Opaque: true, TypeFunction.CIdentifier: not "intern" };
    }

    public static bool IsOpaqueUntyped(GirModel.Record record)
    {
        //A CIdentifier "intern" means that this type is fundamental and can be treated as
        //untyped.
        return record is { Foreign: false, Opaque: true, TypeFunction: null or { CIdentifier: "intern" } };
    }

    public static bool IsTyped(GirModel.Record record)
    {
        //Even if there is a TypeFunction it does not mean that it actually is
        //a typed / boxed record. There is a magic keyword "intern" which means this
        //record is actually fundamental and does not have a type function.
        return record is { Foreign: false, Opaque: false, TypeFunction.CIdentifier: not "intern" };
    }

    public static bool IsUntyped(GirModel.Record record)
    {
        return record is { Foreign: false, Opaque: false, TypeFunction: null or { CIdentifier: "intern" } };
    }

    public static string GetFullyQualifiedInternalStructName(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalStructName(record);

    public static string GetFullyQualifiedInternalHandle(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalHandleName(record);

    public static string GetFullyQualifiedInternalNullHandleInstance(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalNullHandleName(record) + "." + "Instance";

    public static string GetFullyQualifiedInternalOwnedHandle(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalOwnedHandleName(record);

    public static string GetFullyQualifiedInternalUnownedHandle(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalUnownedHandleName(record);

    public static string GetFullyQualifiedInternalManagedHandleCreateMethod(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + GetInternalManagedHandleName(record) + ".Create";

    public static string GetFullyQualifiedPublicClassName(GirModel.Record record)
        => Namespace.GetPublicName(record.Namespace) + "." + GetPublicClassName(record);

    public static string GetPublicClassName(GirModel.Record record)
        => record.Name;

    public static string GetInternalStructName(GirModel.Record record)
        => record.Name + "Data";

    public static string GetInternalHandleName(GirModel.Record record)
        => record.Name + "Handle";

    public static string GetInternalNullHandleName(GirModel.Record record)
        => record.Name + "NullHandle";

    public static string GetInternalOwnedHandleName(GirModel.Record record)
        => record.Name + "OwnedHandle";

    public static string GetInternalUnownedHandleName(GirModel.Record record)
        => record.Name + "UnownedHandle";

    public static string GetInternalManagedHandleName(GirModel.Record record)
        => record.Name + "ManagedHandle";

    public static bool IsGLibError(GirModel.Record record)
        => record.Namespace.Name == "GLib" && record.Name == "Error";
}
