using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class OpaqueTypedRecord : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var typeName = ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyType.AsT0);

        return new RenderableReturnType(typeName + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.Is<GirModel.Record>(out var record) && Model.Record.IsOpaqueTyped(record);
}
