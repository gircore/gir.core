using System;

namespace Generator.Renderer.Public.Parameter;

internal class Record : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Record>(out var record) && Model.Record.IsStandard(record);
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return parameter.Direction switch
        {
            GirModel.Direction.In => InRecord(parameter),
            GirModel.Direction.Out => OutRecord(parameter),
            _ => throw new Exception($"Unsupported record direction {parameter.Direction}")
        };
    }

    private ParameterTypeData OutRecord(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: ParameterDirection.Out(),
            NullableTypeName: Model.Type.Pointer
        );
    }

    private static ParameterTypeData InRecord(GirModel.Parameter parameter)
    {
        var type = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT0;

        return new ParameterTypeData(
            Direction: ParameterDirection.In(),
            NullableTypeName: Model.ComplexType.GetFullyQualified(type) + Nullable.Render(parameter)
        );
    }
}
