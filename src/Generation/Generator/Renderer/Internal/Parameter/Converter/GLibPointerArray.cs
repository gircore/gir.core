using System;

namespace Generator.Renderer.Internal.Parameter;

public class GLibPointerArray : ParameterConverter
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
            { Direction: GirModel.Direction.Out, Transfer: GirModel.Transfer.None } => Model.PointerArrayType.GetFullyQuallifiedHandle(),
            _ => throw new Exception($"ptrarray parameter type {parameter.Name}: CallerAllocates={parameter.CallerAllocates} Direction={parameter.Direction} Transfer={parameter.Transfer} not yet supported")
        };
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.In(), //We can use "in" here because caller allocates the memory
        _ => throw new Exception($"Unknown parameter direction for ptrarray parameter {parameter.Name}")
    };
}

