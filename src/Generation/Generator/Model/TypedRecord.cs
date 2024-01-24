using System.Linq;

namespace Generator.Model;

internal static class TypedRecord
{
    public static string GetPublicClassName(GirModel.Record record)
        => record.Name;

    public static string GetFullyQualifiedPublicClassName(GirModel.Record record)
        => Namespace.GetPublicName(record.Namespace) + "." + GetPublicClassName(record);

    public static string GetFullyQualifiedInternalClassName(GirModel.Record record)
        => Namespace.GetInternalName(record.Namespace) + "." + record.Name;

    public static string GetInternalHandle(GirModel.Record record)
        => $"{Type.GetName(record)}Handle";

    public static string GetInternalManagedHandle(GirModel.Record record)
        => $"{Type.GetName(record)}ManagedHandle";

    public static string GetInternalOwnedHandle(GirModel.Record record)
        => $"{Type.GetName(record)}OwnedHandle";

    public static string GetInternalUnownedHandle(GirModel.Record record)
        => $"{Type.GetName(record)}UnownedHandle";

    public static string GetFullyQuallifiedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalHandle(record)}";

    public static string GetFullyQuallifiedOwnedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalOwnedHandle(record)}";

    public static string GetFullyQuallifiedUnownedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalUnownedHandle(record)}";

    public static string GetFullyQuallifiedNullHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalUnownedHandle(record)}.NullHandle";

    public static string GetDataName(GirModel.Record record)
        => $"{Type.GetName(record)}Data";

    public static string GetFullyQuallifiedDataName(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetDataName(record)}";

    public static string GetFullyQuallifiedManagedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalManagedHandle(record)}";

    public static string GetInternalArrayHandle(GirModel.Record record)
    {
        var prefix = $"{Type.GetName(record)}Array";
        if (record.Namespace.Records.Select(x => x.Name).Contains(prefix))
            prefix += "2";

        return $"{prefix}Handle";
    }

    public static string GetFullyQuallifiedArrayHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalArrayHandle(record)}";

    public static string GetInternalArrayOwnedHandle(GirModel.Record record)
    {
        var prefix = $"{Type.GetName(record)}Array";
        if (record.Namespace.Records.Select(x => x.Name).Contains(prefix))
            prefix += "2";

        return $"{prefix}OwnedHandle";
    }

    public static string GetFullyQuallifiedArrayOwnedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalArrayOwnedHandle(record)}";

    public static string GetInternalArrayUnownedHandle(GirModel.Record record)
    {
        var prefix = $"{Type.GetName(record)}Array";
        if (record.Namespace.Records.Select(x => x.Name).Contains(prefix))
            prefix += "2";
        return $"{prefix}UnownedHandle";
    }

    public static string GetFullyQuallifiedArrayUnownedHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalArrayUnownedHandle(record)}";

    public static string GetFullyQuallifiedArrayNullHandle(GirModel.Record record)
        => $"{Namespace.GetInternalName(record.Namespace)}.{GetInternalArrayUnownedHandle(record)}.NullHandle";
}
