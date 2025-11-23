using System;

namespace Generator.Renderer.Internal.Parameter;

public class PointerGLibPtrArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibPtrArray();
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
        return parameter switch
        {
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.None } => Model.PointerArrayType.GetFullyQuallifiedHandle(),
            { Direction: GirModel.Direction.In, Transfer: GirModel.Transfer.Full } => Model.PointerArrayType.GetFullyQuallifiedUnownedHandle(),
            _ => throw new Exception($"Can't detect ptrarray parameter type {parameter.Name}: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer}")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        _ => throw new Exception($"Unknown parameter direction for ptrarray parameter {parameter.Name}")
    };
}

