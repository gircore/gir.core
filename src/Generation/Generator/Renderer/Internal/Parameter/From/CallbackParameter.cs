using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class CallbackParameter
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    //Internal callbacks are not nullable
    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Callback) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return Namespace.GetInternalName(type.Namespace) + "." + Type.GetName((GirModel.Type) type);
    }
}
