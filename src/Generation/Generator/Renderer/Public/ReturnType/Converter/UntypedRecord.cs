using System;

namespace Generator.Renderer.Public.ReturnType;

internal class UntypedRecord : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        if (returnType.Transfer == GirModel.Transfer.Container)
            throw new NotSupportedException($"Can't return untyped record {returnType} with transfer mode container");

        var typeName = Model.ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(typeName + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
}
