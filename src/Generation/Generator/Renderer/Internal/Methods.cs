using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class Methods
{
    public static string Render(IEnumerable<GirModel.Method> methods)
    {
        return methods
            .Where(Method.IsEnabled)
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Method? method)
    {
        if (method is null)
            return string.Empty;

        try
        {
            var separator = method.Parameters.Any() ? ", " : string.Empty;

            return $@"
{DocComments.Render($"Calls native method {method.CIdentifier}.", DocComments.RenderVersion(method.Version))}
{DocComments.Render(method.Parameters)}
{DocComments.Render(method.ReturnType)}
{VersionAttribute.Render(method.Version)}
[DllImport(ImportResolver.Library, EntryPoint = ""{method.CIdentifier}"")]
public static extern {ReturnType.Render(method.ReturnType)} {Method.GetInternalName(method)}({InstanceParameters.Render(method.InstanceParameter)}{separator}{Parameters.Render(method.Parameters)});";
        }
        catch (Exception e)
        {
            Log.Warning($"Did not generate internal method '{method.Parent.Name}.{Method.GetPublicName(method)}': {e.Message}");

            return string.Empty;
        }
    }
}
