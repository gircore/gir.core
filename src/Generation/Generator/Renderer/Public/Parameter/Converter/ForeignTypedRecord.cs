using System;

namespace Generator.Renderer.Public.Parameter;

internal class ForeignTypedRecord : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);
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
        var type = (GirModel.Record) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT0.Type;
        return Model.ForeignTypedRecord.GetFullyQualifiedPublicClassName(type) + Nullable.Render(parameter);
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        _ => throw new Exception("Foreign typed records with direction != in not yet supported")
    };
}
