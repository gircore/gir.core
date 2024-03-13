using System;
using Generator.Model;
namespace Generator.Renderer.Public.ReturnType;

internal class UntypedRecord : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        if (returnType.Transfer == GirModel.Transfer.Container)
            throw new NotSupportedException($"Can't return untyped record {returnType} with transfer mode container");

        var typeName = ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyType.AsT0);

        return new RenderableReturnType(typeName + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Record>(out var record) && Model.Record.IsUntyped(record);
}
