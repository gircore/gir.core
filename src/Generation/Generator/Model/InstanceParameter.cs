namespace Generator.Model;

internal static class InstanceParameter
{
    public static string GetName(GirModel.InstanceParameter instanceParameter)
    {
        return instanceParameter.Name.ToCamelCase().EscapeIdentifier();
    }
}
