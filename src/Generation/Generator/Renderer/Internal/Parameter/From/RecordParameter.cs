using System;
using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class RecordParameter
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    //Native records are represented as SafeHandles and are not nullable
    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return parameter switch
        {
            { Direction: GirModel.Direction.In } => Record.GetFullyQualifiedInternalHandle(type),
            { CallerAllocates: true } => Record.GetFullyQualifiedInternalHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.Full } => Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.Container } => Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.InOut, Transfer: GirModel.Transfer.None } => Record.GetFullyQualifiedInternalUnownedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.Full } => Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.Container } => Record.GetFullyQualifiedInternalOwnedHandle(type),
            { CallerAllocates: false, Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.None } => Record.GetFullyQualifiedInternalUnownedHandle(type),
            _ => throw new Exception($"Can't detect parameter type: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
