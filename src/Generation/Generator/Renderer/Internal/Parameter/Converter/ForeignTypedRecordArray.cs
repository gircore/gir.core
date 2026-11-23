using System;

namespace Generator.Renderer.Internal.Parameter;

internal class ForeignTypedRecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (!parameter.AnyTypeReferenceOrVarArgs.AsT0.AsArrayTypeReference.IsPointer)
        {
            var record = (GirModel.Record) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsArrayTypeReference.AnyTypeReference.AsTypeReference.Type;
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
