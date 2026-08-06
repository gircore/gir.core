using System;

namespace Generator.Renderer.Internal.Parameter;

internal class UntypedRecordCallbackArray : ParameterConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
    {
        return anyTypeReference.ReferencesArray<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeReferenceOrVarArgs.AsT0.AsT1.IsPointer)
            return PointerArray(parameter);

        return StructArray(parameter);
    }

    private static RenderableParameter PointerArray(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: $"ref {Model.Type.Pointer}",
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static RenderableParameter StructArray(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.Type.Pointer,
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
