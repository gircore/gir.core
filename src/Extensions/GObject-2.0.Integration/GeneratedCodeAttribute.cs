using System;
using System.Reflection;

namespace GObject.Integration;

internal static class GeneratedCodeAttribute
{
    private static string Version { get; } = GetVersion();
    private static string Name { get; } = GetName();

    public static string Render()
    {
        return $"""[System.CodeDom.Compiler.GeneratedCode("{Name}", "{Version}")]""";
    }
    
    private static string GetName()
    {
        var name = Assembly.GetExecutingAssembly().GetName().Name;
        if(name is null)
            throw new NotSupportedException("Could not get assembly version");

        return $"GirCore.{name}";
    }
    
    private static string GetVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        if(version is null)
            throw new NotSupportedException("Could not get assembly version");
        
        return $"{version.Major}.{version.Minor}.{version.Build}";
    }
}