using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class StringArray : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(ArrayType.GetName(returnType.AnyType.AsT1) + Nullable.Render(returnType));
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyType.IsArray<GirModel.String>();
}
