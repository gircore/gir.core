using System;

namespace Generator.Renderer.Internal.Parameter;

internal class OpaqueUntypedRecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsOpaqueUntyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (!parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.IsPointer)
        {
            var record = (GirModel.Record) parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.AnyTypeReference.AsT0.Type;
            throw new Exception($"Unpointed opaque untyped record array of type {record.Name} not yet supported");
        }

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: $"ref {Model.Type.Pointer}",
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
