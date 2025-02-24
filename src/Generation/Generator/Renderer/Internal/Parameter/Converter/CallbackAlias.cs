namespace Generator.Renderer.Internal.Parameter;

internal class CallbackAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsAlias<GirModel.Callback>();
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
        var type = (GirModel.Callback) ((GirModel.Alias) parameter.AnyTypeOrVarArgs.AsT0.AsT0).Type;
        return Model.Namespace.GetInternalName(type.Namespace) + "." + Model.Type.GetName(type);
    }
}
