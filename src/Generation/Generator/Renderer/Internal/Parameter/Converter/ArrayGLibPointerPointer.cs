namespace Generator.Renderer.Internal.Parameter;

public class ArrayGLibPointerPointer : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibPtrArray<GirModel.Pointer>();
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
        var arrayType = parameter.AnyTypeOrVarArgs.AsT0.AsT1;

        return arrayType.Length is null
            ? Model.Type.PointerArray
            : Model.Record.GetFullyQualifiedInternalStructName((GirModel.Record) arrayType.AnyType.AsT0) + "[]";
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length switch
        {
            { } length => MarshalAs.UnmanagedLpArray(sizeParamIndex: length),
            _ => string.Empty,
        };
    }
}

