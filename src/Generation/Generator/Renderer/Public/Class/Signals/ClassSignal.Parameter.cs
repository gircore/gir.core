using System.Collections.Generic;
using System.Text;

namespace Generator.Renderer.Public;

public static partial class ClassSignal
{
    private static string RenderAsSignalParammeters(IEnumerable<GirModel.Parameter> parameters)
    {
        var sb = new StringBuilder();
        var index = 1; //Argument 0 is reserved

        foreach (var parameter in parameters)
        {
            sb.AppendLine(RenderAsSignalParammeter(parameter, index));
            index++;
        }

        return sb.ToString();
    }

    private static string RenderAsSignalParammeter(GirModel.Parameter parameter, int index)
    {
        var p = ParameterRenderer.Render(parameter);

        return $@"public {p.NullableTypeName} {parameter.Name.ToPascalCase()} => Extract<{p.NullableTypeName}>(Args[{index}]);";
    }
}
