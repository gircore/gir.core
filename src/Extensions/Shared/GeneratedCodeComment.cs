using System;
using System.Reflection;

internal static class GeneratedCodeComment
{
    private static string Version { get; } = GetVersion();
    private static string Name { get; } = GetName();

    public static string Render()
    {
        //TODO: Use GeneratedCodeAttribute in case https://github.com/dotnet/runtime/issues/122718 is fixed.
        return $"//GeneratedCode by {Name} Version: {Version}";
    }

    private static string GetName()
    {
        var name = Assembly.GetExecutingAssembly().GetName().Name;
        if (name is null)
            throw new NotSupportedException("Could not get assembly version");

        return $"GirCore.{name}";
    }

    private static string GetVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        if (version is null)
            throw new NotSupportedException("Could not get assembly version");

        return $"{version.Major}.{version.Minor}.{version.Build}";
    }
}
