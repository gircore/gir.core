namespace Generator.Model;

internal static class Parameter
{
    public static string GetName(GirModel.Parameter parameter)
    {
        return parameter.Name.ToCamelCase().EscapeIdentifier();
    }

    public static string GetConvertedName(GirModel.Parameter parameter)
    {
        return GetName(parameter) + "Native";
    }
}
