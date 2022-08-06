using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal;

internal static class DocComments
{
    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
            .Select(Render)
            .Join(Environment.NewLine);
    }

    private static string Render(GirModel.Parameter parameter) =>
        $@"/// <param name=""{parameter.Name}"">Transfer ownership: {parameter.Transfer} Nullable: {parameter.Nullable}</param>";

    public static string Render(GirModel.ReturnType returnType) =>
        $@"/// <returns>Transfer ownership: {returnType.Transfer} Nullable: {returnType.Nullable}</returns>";
}
