using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class TypedRecordArray : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(ArrayType.GetName(returnType.AnyType.AsT1));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.IsArray<GirModel.Record>(out var record) && Model.Record.IsTyped(record);
}
