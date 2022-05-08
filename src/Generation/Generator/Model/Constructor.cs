namespace Generator.Model;

internal static class Constructor
{
    public static string GetName(GirModel.Constructor constructor)
    {
        return constructor.Name.ToPascalCase().EscapeIdentifier();
    }
}
