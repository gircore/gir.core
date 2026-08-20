using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class ForeignTypedRecord : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var typeName = ComplexType.GetFullyQualified((GirModel.Record) returnType.AnyTypeReference.AsT0.Type);

        return new RenderableReturnType(typeName + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.References<GirModel.Record>(out var record) && Model.Record.IsForeignTyped(record);
}
