namespace Generator.Model;

internal static class Constant
{
    public static string GetName(GirModel.Constant constant)
    {
        return constant.Name.EscapeIdentifier();
    }
}
