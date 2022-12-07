using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Internal;

internal static class Callback
{
    public static string Render(GirModel.Callback callback)
    {
        try
        {
            return $"public delegate {ReturnType.RenderForCallback(callback.ReturnType)} {Model.Callback.GetInternalDelegateName(callback)}({RenderParameters(callback.Parameters)});";
        }
        catch (System.Exception ex)
        {
            Log.Warning($"Could not render internal callback: {callback.Name}: {ex.Message}");
            return string.Empty;
        }
    }

    private static string RenderParameters(IEnumerable<GirModel.Parameter> parameters)
    {
        return parameters
                .Select(RenderableCallbackParameterFactory.Create)
                .Select(parameter => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}")
                .Join(", ");
    }
}
