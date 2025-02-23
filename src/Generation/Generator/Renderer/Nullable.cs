namespace Generator.Renderer;

internal static class Nullable
{
    public static string Render(GirModel.Nullable nullable)
    {
        return nullable.Nullable ? "?" : string.Empty;
    }
}
