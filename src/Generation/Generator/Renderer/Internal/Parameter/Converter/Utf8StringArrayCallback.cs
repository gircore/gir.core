namespace Generator.Renderer.Internal.Parameter;

internal class Utf8StringArrayCallback : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Utf8String>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        return parameter switch
        {
            // Arrays of string which do not transfer ownership and have no length index can not be marshalled automatically
            { Transfer: GirModel.Transfer.None, AnyTypeOrVarArgs.AsT0.AsT1.Length: null } => Model.Type.Pointer,
            _ => Model.ArrayType.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT1)
        };
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length switch
        {
            null => string.Empty,
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l)
        };
    }
}
