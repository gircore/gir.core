using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class Constructors
{
    public static string Render(IEnumerable<GirModel.Constructor> constructors)
    {
        return constructors
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Constructor? constructor)
    {
        if (constructor is null)
            return string.Empty;

        return @$"
/// <summary>
/// Calls native constructor {constructor.CIdentifier}.
/// </summary>
{DocComments.Render(constructor.Parameters)}
{DocComments.Render(constructor.ReturnType)}
[DllImport(ImportResolver.Library, EntryPoint = ""{ constructor.CIdentifier }"")]
public static extern { ReturnType.Render(constructor.ReturnType) } { Constructor.GetName(constructor) }({ Parameters.Render(constructor.Parameters) });";
    }
}
