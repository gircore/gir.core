namespace Generator.Renderer;

internal static class VersionAttribute
{
    public static string Render(string? version)
    {
        return version is null
            ? string.Empty
            : $"[Version(\"{version}\")]";
    }
}
