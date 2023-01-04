using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class MethodRenderer
{
    public static string Render(GirModel.Method method)
    {
        try
        {
            var parameters = ParameterToNativeExpression.Initialize(method.Parameters);

            return @$"
{VersionAttribute.Render(method.Version)}
public {ReturnType.Render(method.ReturnType)} {Method.GetPublicName(method)}({RenderParameters(parameters)})
{{
    {RenderMethodContent(parameters)}
    {RenderCallStatement(method, parameters, out var resultVariableName)}
    {RenderReturnStatement(method, resultVariableName)}
}}";
        }
        catch (Exception e)
        {
            var message = $"Did not generate method '{method.Parent.Name}.{Method.GetPublicName(method)}': {e.Message}";

            if (e is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static string RenderMethodContent(IEnumerable<ParameterToNativeData> parameters)
    {
        return parameters
            .Select(x => x.GetExpression())
            .Where(x => !string.IsNullOrEmpty(x))
            .Cast<string>()
            .Join(Environment.NewLine);
    }

    private static string RenderParameters(IEnumerable<ParameterToNativeData> parameters)
    {
        var result = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.IsClosure)
                continue;

            if (parameter.IsDestroyNotify)
                continue;

            var typeData = RenderableParameterFactory.Create(parameter.Parameter);
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }

    private static string RenderCallStatement(GirModel.Method method, IReadOnlyList<ParameterToNativeData> parameters, out string resultVariableName)
    {
        resultVariableName = "result";
        var call = new StringBuilder();

        if (!method.ReturnType.AnyType.Is<GirModel.Void>())
            call.Append($"var {resultVariableName} = ");

        call.Append($"{Namespace.GetInternalName(method.Parent.Namespace)}.{method.Parent.Name}.{Method.GetInternalName(method)}(");
        call.Append("this.Handle" + (parameters.Any() ? ", " : string.Empty));
        call.Append(string.Join(", ", parameters.Select(x => x.GetCallName())));
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
