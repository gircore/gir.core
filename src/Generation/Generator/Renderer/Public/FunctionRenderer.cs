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

        if (!IsSupported(function))
            return string.Empty;

        try
        {
            var parameters = ParameterToNativeExpression.Initialize(function.Parameters);
            var newModifier = Function.HidesFunction(function) ? "new " : string.Empty;
            return @$"
{VersionAttribute.Render(function.Version)}
public static {newModifier}{ReturnTypeRenderer.Render(function.ReturnType)} {Function.GetName(function)}({RenderParameters(parameters)})
{{
    {RenderFunctionContent(parameters)}
    {RenderCallStatement(function, parameters, out var resultVariableName)}
    {RenderPostCallContent(parameters)}
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
        call.Append(Error.RenderParameter(function));
        call.Append(");\n");

        call.Append(Error.RenderThrowOnError(function));

        return call.ToString();
    }

    private static string RenderReturnStatement(GirModel.Function function, string returnVariable)
    {
        return function.ReturnType.AnyType.Is<GirModel.Void>()
            ? string.Empty
            : $"return {ReturnTypeToManagedExpression.Render(function.ReturnType, returnVariable)};";
    }

    private static bool IsSupported(GirModel.Function function)
    {
        var parameter = function.Parameters.FirstOrDefault(x => x is { Direction: GirModel.Direction.InOut }
                                                               && x.AnyTypeOrVarArgs.TryPickT0(out var anyType, out _)
                                                               && anyType.TryPickT1(out var arrayType, out _)
                                                               && arrayType.Length is not null);

        if (parameter is null)
            return true;

        var lengthParameter = function.Parameters.ElementAt(parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length!.Value);
        if (lengthParameter.Direction == GirModel.Direction.InOut)
        {
            Log.Warning($"Skipping public function {function.CIdentifier} as it has a size parameter which is defined as inout parameter which is not supported currently.");
            return false;
        }

        return true;
    }
}
