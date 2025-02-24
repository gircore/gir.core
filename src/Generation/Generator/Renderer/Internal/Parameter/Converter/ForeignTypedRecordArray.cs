using System;

namespace Generator.Renderer.Internal.Parameter;

internal class ForeignTypedRecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (!parameter.AnyTypeOrVarArgs.AsT0.AsT1.IsPointer)
        {
            var record = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0;
            throw new Exception($"Unpointed foreign record array of type {record.Name} not yet supported");
        }

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: $"ref {Model.Type.Pointer}",
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
