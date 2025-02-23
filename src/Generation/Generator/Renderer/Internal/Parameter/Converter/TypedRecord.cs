using System;

namespace Generator.Renderer.Internal.Parameter;

internal class TypedRecord : ParameterConverter
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
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        //Native records are represented as SafeHandles and are not nullable

        var type = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return parameter switch
        {
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.None } => Model.TypedRecord.GetFullyQuallifiedHandle(type),
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.Full } => Model.TypedRecord.GetFullyQuallifiedUnownedHandle(type),
            _ => throw new Exception($"Can't detect record parameter type {parameter.Name}: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        _ => throw new Exception($"Unknown parameter direction for opaque typed record parameter {parameter.Name}")
    };
}
