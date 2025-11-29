using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class GLibPointerArray : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return returnType.AnyType.IsGLibPtrArray();
    }

    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        return new RenderableReturnType(Model.PointerArrayType.GetFullyQualifiedPublicClassName() + Nullable.Render(returnType));
    }
}
