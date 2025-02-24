using System;

namespace Generator.Renderer.Internal.Parameter;

internal class TypedRecordArray : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsArray<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.AsT0.AsT1.IsPointer)
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
        var record = (GirModel.Record) parameter.AnyTypeOrVarArgs.AsT0.AsT1.AnyType.AsT0;

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Model.TypedRecord.GetFullyQuallifiedArrayHandle(record),
            Name: Model.Parameter.GetName(parameter)
        );
    }
}
