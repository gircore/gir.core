using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Generator.Model;

namespace Generator.Renderer.Public;

public static class InterfaceMethods
{
    public static string RenderMethods(this GirModel.Interface @interface)
    {
        return @interface.Methods
            .Where(Method.IsEnabled)
            .Select(RenderMethodDefinition)
            .Join(Environment.NewLine);
    }

    private static string RenderMethodDefinition(GirModel.Method method)
    {
        try
        {
            var parameters = ParameterToNativeExpression.Initialize(method.Parameters);

            return $"""

                    {VersionAttribute.Render(method.Version)}
                    {ReturnTypeRenderer.Render(method.ReturnType)} {Method.GetPublicName(method)}({RenderParameters(parameters)});
                    """;
        }
        catch (Exception ex)
        {
            Log.Warning($"Could not render interface method definition {method.Name}: {ex.Message}");
            return string.Empty;
        }
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
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {parameter.GetSignatureName()}");
        }

        return result.Join(", ");
    }
}
