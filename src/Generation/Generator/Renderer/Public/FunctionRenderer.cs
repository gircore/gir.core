using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

internal static class FunctionRenderer
{
    public static string Render(GirModel.Function? function)
    {
        if (function is null)
            return string.Empty;

        try
        {
            var callableData = CallableExpressions.Initialize(function);
            var newModifier = Function.HidesFunction(function) ? "new " : string.Empty;
            return @$"
{VersionAttribute.Render(function.Version)}
public static {newModifier}{ReturnTypeRenderer.Render(function.ReturnType)} {Function.GetName(function)}({RenderParameters(callableData.ParameterToNativeDatas)})
{{
    {RenderFunctionContent(callableData.ParameterToNativeDatas)}
    {RenderCallStatement(function, callableData.ParameterToNativeDatas, out var resultVariableName)}
    {RenderPostCallContent(callableData.ParameterToNativeDatas)}
    {callableData.ReturnTypeToManagedData.GetPostReturnStatement(resultVariableName)}
    {RenderReturnStatement(callableData.ReturnTypeToManagedData, resultVariableName)}
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

    private static string RenderPostCallContent(IEnumerable<ParameterToNativeData> parameters)
    {
        return parameters
            .Select(x => x.GetPostCallExpression())
            .Where(x => !string.IsNullOrEmpty(x))
            .Cast<string>()
            .Join(Environment.NewLine);
    }

    private static string RenderParameters(IEnumerable<ParameterToNativeData> parameters)
    {
        var result = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.IsCallbackUserData)
                continue;

            if (parameter.IsDestroyNotify)
                continue;

            if (parameter.IsArrayLengthParameter)
                continue;

            if (parameter.IsGLibErrorParameter)
                continue;

            var typeData = ParameterRenderer.Render(parameter.Parameter);
            var direction = parameter.IsInOutArrayLengthParameter
                ? ParameterDirection.Out()
                : typeData.Direction;

            result.Add($"{direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }

    private static string RenderCallStatement(GirModel.Function function, IEnumerable<ParameterToNativeData> parameters, out string resultVariableName)
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
        call.Append(Error.RenderParameter(function));
        call.Append(");\n");

        call.Append(Error.RenderThrowOnError(function));

        return call.ToString();
    }

    private static string RenderReturnStatement(ReturnTypeToManagedData data, string returnVariable)
    {
        return data.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {data.GetExpression(returnVariable)};";
    }
}
