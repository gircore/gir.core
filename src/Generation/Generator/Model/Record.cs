namespace Generator.Model;

internal static partial class Record
{
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
