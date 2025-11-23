namespace Generator.Model;

internal static class PointerArrayType
{
    public static string GetPublicClassName()
        => "PtrArray";

    public static string GetInternalName()
        => "PtrArray";

    public static string GetInternalNamespace()
        => "GLib.Internal";

    public static string GetPublicNamespace()
        => "GLib";

    public static string GetFullyQualifiedPublicClassName()
        => $"{GetPublicNamespace()}.{GetPublicClassName()}";

    public static string GetInternalHandle()
        => $"{GetInternalName()}Handle";

    public static string GetInternalOwnedHandle()
        => $"{GetInternalName()}OwnedHandle";

    public static string GetInternalUnownedHandle()
        => $"{GetInternalName()}UnownedHandle";

    public static string GetFullyQuallifiedHandle()
        => $"{GetInternalNamespace()}.{GetInternalHandle()}";

    public static string GetFullyQuallifiedOwnedHandle()
        => $"{GetInternalNamespace()}.{GetInternalOwnedHandle()}";

    public static string GetFullyQuallifiedUnownedHandle()
        => $"{GetInternalNamespace()}.{GetInternalUnownedHandle()}";

    public static string GetFullyQuallifiedNullHandle()
        => $"{GetInternalNamespace()}.{GetInternalUnownedHandle()}.NullHandle";
}
