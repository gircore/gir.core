using System;

namespace Generator.Renderer.Public.Parameter;

internal class PrimitiveValueType : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.PrimitiveValueType>();
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: Model.Type.GetName(parameter.AnyTypeOrVarArgs.AsT0.AsT0)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In, IsPointer: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.In, IsPointer: false } => ParameterDirection.In(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => throw new Exception($"Could not determin direction of public primitive value type parameter {parameter}.")
    };
}
