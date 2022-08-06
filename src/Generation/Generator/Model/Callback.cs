namespace Generator.Model;

internal static class Callback
{
    public static string GetInternalDelegateName(GirModel.Callback callback)
    {
        return callback.Name.ToPascalCase() + "Callback";
    }
}
