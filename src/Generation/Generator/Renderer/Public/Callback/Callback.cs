using System.Collections.Generic;

namespace Generator.Renderer.Public;

internal static class Callback
{
    public static string Render(GirModel.Callback callback)
    {
        //We use the internal parameter expression function as for callbacks the data must
        //not be transferred to native, but to the managed world.
        var parameters = Internal.ParameterToManagedExpression.Initialize(callback.Parameters);

        return $"public delegate {ReturnTypeRendererCallback.Render(callback.ReturnType)} {callback.Name}({RenderParameters(parameters)});";
    }

    private static string RenderParameters(IEnumerable<Internal.ParameterToManagedData> parameters)
    {
        var result = new List<string>();
        foreach (var parameter in parameters)
        {
            if (parameter.IsCallbackUserData)
                continue;

            if (parameter.IsArrayLengthParameter)
                continue;

            if (parameter.IsGLibErrorParameter)
                continue;

            var name = Model.Parameter.GetName(parameter.Parameter);
            var typeData = ParameterRenderer.Render(parameter.Parameter);
            result.Add($"{typeData.Direction}{typeData.NullableTypeName} {name}");
        }

        return result.Join(", ");
    }
}
