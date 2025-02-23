namespace Generator.Model;

internal static class Field
{
    public static string GetName(GirModel.Field field)
    {
        return field.Name.ToPascalCase();
    }
}
