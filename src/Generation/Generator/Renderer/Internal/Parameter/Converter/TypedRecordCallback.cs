using System;

namespace Generator.Renderer.Internal.Parameter;

internal class TypedRecordCallback : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => throw new Exception("Unknown direction for record parameter in callback")
    };
}
