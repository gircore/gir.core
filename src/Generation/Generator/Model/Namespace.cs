namespace Generator.Model;

internal static class Namespace
{
    public static string GetPublicName(GirModel.Namespace @namespace)
        => @namespace.Name.ToPascalCase().EscapeIdentifier();

    public static string GetCanonicalName(GirModel.Namespace @namespace)
        => $"{@namespace.Name}-{@namespace.Version}";

    public static string GetInternalName(GirModel.Namespace @namespace)
        => $"{GetPublicName(@namespace)}.Internal";
}
