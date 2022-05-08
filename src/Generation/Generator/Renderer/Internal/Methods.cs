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

        var separator = method.Parameters.Any() ? ", " : string.Empty;

        return $@"
/// <summary>
/// Calls native method {method.CIdentifier}.
/// </summary>
{DocComments.Render(method.Parameters)}
{DocComments.Render(method.ReturnType)}
[DllImport(ImportResolver.Library, EntryPoint = ""{ method.CIdentifier }"")]
public static extern { ReturnType.Render(method.ReturnType) } { Method.GetInternalName(method) }({InstanceParameters.Render(method.InstanceParameter)}{separator}{ Parameters.Render(method.Parameters) });";
    }
}
