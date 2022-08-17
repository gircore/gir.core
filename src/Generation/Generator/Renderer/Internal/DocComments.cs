using System;
using System.Collections.Generic;
using System.Linq;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class DocComments
{
    public static string Render(params string[] text)
    {
        return $@"/// <summary>
///{text.Where(x => x != string.Empty).Join(Environment.NewLine + "///")}
/// </summary>";
    }

    public static string RenderVersion(string? version)
    {
        return version is null
            ? string.Empty
            : $"Version: {version}";
    }

    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Parameter parameter) =>
        $@"/// <param name=""{Parameter.GetName(parameter)}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";

    public static string Render(GirModel.ReturnType returnType) =>
        $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";
}
