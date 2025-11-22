using System;

namespace Generator.Renderer.Internal.Parameter;

internal class StringGLibPointerArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsGLibPtrArray<GirModel.String>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: GetAttribute(parameter),
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetAttribute(GirModel.Parameter parameter)
    {
        return parameter.AnyTypeOrVarArgs.AsT0.AsT1.Length switch
        {
            null => string.Empty,
            { } l => MarshalAs.UnmanagedLpArray(sizeParamIndex: l)
        };
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
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out, CallerAllocates: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
