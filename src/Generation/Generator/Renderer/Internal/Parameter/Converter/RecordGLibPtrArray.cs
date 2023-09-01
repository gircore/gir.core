namespace Generator.Renderer.Internal.Parameter;

public class RecordGLibPtrArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibPtrArray<GirModel.Record>(out var record) && !Model.Record.IsOpaqueTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: string.Empty,
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
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

