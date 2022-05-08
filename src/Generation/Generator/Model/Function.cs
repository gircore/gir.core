namespace Generator.Model;

internal static class Function
{
    public static string GetName(GirModel.Function function)
    {
        return function.Name.ToPascalCase().EscapeIdentifier();
    }
}
