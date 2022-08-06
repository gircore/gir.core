using System.Collections.Generic;
using System.Text;

namespace Generator.Renderer.Public;

internal static class ParametersToNativeExpression
{
    public static string Render(IEnumerable<GirModel.Parameter> parameters, out IEnumerable<string> parameterNames)
    {
        var expressions = new StringBuilder();
        var names = new List<string>();

        foreach (var p in parameters)
        {
            var expression = p.ToNative(out var variableName);
            if (expression is not null)
                expressions.AppendLine(expression);

            names.Add(variableName);
        }

        parameterNames = names;

        return expressions.ToString();
    }
}
