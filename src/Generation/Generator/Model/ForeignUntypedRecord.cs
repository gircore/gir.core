namespace Generator.Model;

internal static class ForeignUntypedRecord
{
    public static string GetPublicClassName(GirModel.Record record)
        => record.Name;

    public static string GetFullyQualifiedPublicClassName(GirModel.Record record)
        => Namespace.GetPublicName(record.Namespace) + "." + GetPublicClassName(record);

    public static string GetFullyQualifiedInternalClassName(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + record.Name;

    public static string GetInternalHandle(GirModel.Record record)
        => $"{Type.GetName(record)}Handle";

    public static string GetInternalOwnedHandle(GirModel.Record record)
        => $"{Type.GetName(record)}OwnedHandle";

    public static string GetInternalUnownedHandle(GirModel.Record record)
        => $"{Type.GetName(record)}UnownedHandle";

    public static string GetFullyQuallifiedInternalHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalHandle(record)}";

    public static string GetFullyQuallifiedOwnedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalOwnedHandle(record)}";

    public static string GetFullyQuallifiedUnownedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalUnownedHandle(record)}";

    public static string GetFullyQuallifiedNullHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalUnownedHandle(record)}.NullHandle";
}
