using System;

namespace Generator.Renderer.Public.Parameter;

internal class UntypedRecord : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
    }

    public ParameterTypeData Create(GirModel.Parameter parameter)
    {
        return new ParameterTypeData(
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        var type = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT0;
        return Model.UntypedRecord.GetFullyQualifiedPublicClassName(type) + Nullable.Render(parameter);
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => throw new Exception($"untyped records with direction '{parameter.Direction}' not yet supported")
    };
}
