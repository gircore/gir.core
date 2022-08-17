using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class MethodRenderer
{
    public static string Render(GirModel.Class cls, GirModel.Method method)
    {
        try
        {
            return @$"
{VersionAttribute.Render(method.Version)}
public {ReturnType.Render(method.ReturnType)} {Method.GetPublicName(method)}({Parameters.Render(method.Parameters)})
{{
    {ParametersToNativeExpression.Render(method.Parameters, out var parameterNames)}
    {RenderCallStatement(cls, method, parameterNames, out var resultVariableName)}
    {RenderReturnStatement(method, resultVariableName)}
}}";
        }
        catch (Exception e)
        {
            var message = $"Did not generate method '{cls.Name}.{Method.GetPublicName(method)}': {e.Message}";

            if (e is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static string RenderCallStatement(GirModel.Class cls, GirModel.Method method, IEnumerable<string> parameterNames, out string resultVariableName)
    {
        resultVariableName = "result";
        var call = new StringBuilder();

        if (!method.ReturnType.AnyType.Is<GirModel.Void>())
            call.Append($"var {resultVariableName} = ");

        call.Append($"Internal.{cls.Name}.{Method.GetInternalName(method)}(");
        call.Append("this.Handle" + (parameterNames.Any() ? ", " : string.Empty));
        call.Append(string.Join(", ", parameterNames));
        call.Append(");\n");

        return call.ToString();
    }

    private static string RenderReturnStatement(GirModel.Method method, string returnVariable)
    {
        return method.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {ReturnTypeToManagedExpression.Render(method.ReturnType, returnVariable)};";
    }
}
