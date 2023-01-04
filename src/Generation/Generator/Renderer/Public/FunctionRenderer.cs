using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class FunctionRenderer
{
    public static string Render(GirModel.Function function)
    {
        try
        {
            var parameters = ParameterToNativeExpression.Initialize(function.Parameters);

            return @$"
{VersionAttribute.Render(function.Version)}
public static {ReturnType.Render(function.ReturnType)} {Function.GetName(function)}({RenderParameters(parameters)})
{{
    {RenderFunctionContent(parameters)}
    {RenderCallStatement(function, parameters, out var resultVariableName)}
    {RenderReturnStatement(function, resultVariableName)}
}}";
        }
        catch (Exception e)
        {
            var message = $"Did not generate function '{Namespace.GetPublicName(function.Namespace)}.Functions.{Function.GetName(function)}': {e.Message}";

            if (e is NotImplementedException)
                Log.Debug(message);
            else
                Log.Warning(message);

            return string.Empty;
        }
    }

    private static string RenderFunctionContent(IEnumerable<ParameterToNativeData> parameters)
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

    private static string RenderCallStatement(GirModel.Function function, IReadOnlyList<ParameterToNativeData> parameters, out string resultVariableName)
    {
        resultVariableName = $"result{Function.GetName(function)}";
        var call = new StringBuilder();

        if (!function.ReturnType.AnyType.Is<GirModel.Void>())
            call.Append($"var {resultVariableName} = ");

        var parent = function.Parent switch
        {
            null => "Functions", //This is a global function which is part of the "Functions" class
            { } p => p.Name
        };

        call.Append($"{Namespace.GetInternalName(function.Namespace)}.{parent}.{Function.GetName(function)}(");
        call.Append(string.Join(", ", parameters.Select(x => x.GetCallName())));
        call.Append(");\n");

        return call.ToString();
    }

    private static string RenderReturnStatement(GirModel.Function function, string returnVariable)
    {
        return function.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {ReturnTypeToManagedExpression.Render(function.ReturnType, returnVariable)};";
    }
}
