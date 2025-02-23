using System;

namespace Generator.Renderer.Internal.Parameter;

internal class UntypedRecord : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
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
        var type = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return parameter switch
        {
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.None } => Model.UntypedRecord.GetFullyQuallifiedHandle(type),
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.Full } => throw new Exception("Ownership transfer for untyped records not supported"),
            { Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.None, CallerAllocates: true } => Model.UntypedRecord.GetFullyQuallifiedOwnedHandle(type),
            _ => throw new Exception($"Can't detect untyped record parameter type {parameter.Name}: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In(),
        _ => throw new Exception($"Unknown parameter direction for untyped record parameter {parameter.Name}")
    };
}
