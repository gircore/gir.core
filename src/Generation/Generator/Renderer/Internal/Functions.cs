using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class Functions
{
    public static string Render(IEnumerable<GirModel.Function> functions)
    {
        return functions
            .Select(Render)
            .Join(Environment.NewLine);
    }

    public static string Render(GirModel.Function? function)
    {
        if (function is null)
            return string.Empty;

        try
        {
            return @$"
{DocComments.Render($"Calls native function {function.CIdentifier}.", DocComments.RenderVersion(function.Version))}
{DocComments.Render(function.Parameters)}
{DocComments.Render(function.ReturnType)}
{PlatformSupportAttribute.Render(function as GirModel.PlatformDependent)}
{VersionAttribute.Render(function.Version)}
[DllImport(ImportResolver.Library, EntryPoint = ""{function.CIdentifier}"")]
public static extern {ReturnType.Render(function.ReturnType)} {Function.GetName(function)}({Parameters.Render(function.Parameters)});";
        }
        catch (Exception ex)
        {
            Log.Warning($"Could not render internal function \"{function.CIdentifier}\": {ex.Message}");
            return string.Empty;
        }
    }
}
