namespace Generator.Renderer.Internal.Parameter;

internal class RecordCallback : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Record>(out var record) && !Model.Record.IsOpaqueTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        var direction = parameter.Direction switch
        {
            GirModel.Direction.Out => ParameterDirection.Out(),
            _ => ParameterDirection.In()
        };

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: direction,
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
