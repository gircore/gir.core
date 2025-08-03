namespace Generator.Renderer.Internal.Parameter;

internal class Callback : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Callback>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    //Internal callbacks are not nullable
    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Callback) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return Model.Namespace.GetInternalName(type.Namespace) + "." + Model.Callback.GetName(type);
    }
}
