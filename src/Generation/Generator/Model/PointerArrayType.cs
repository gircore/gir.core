namespace Generator.Model;

internal static class PointerArrayType
{
    public static string GetPublicClassName()
        => "PtrArray";

    public static string GetFullyQualifiedPublicClassName()
        => "GLib" + "." + GetPublicClassName();
}
