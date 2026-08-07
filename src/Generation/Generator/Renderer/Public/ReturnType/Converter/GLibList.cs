using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class GLibList : ReturnTypeConverter
{
    public bool Supports(GirModel.ReturnType returnType)
    {
        return ListType.SupportsReturnValue(returnType);
    }

    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        ListType.TryGetElementType(returnType, out var elementType);

        //The elements are copied out of the container, so the container itself
        //is not part of the public API.
        var typeName = $"{ListElement.GetPublicTypeName(elementType!)}[]";

        return new RenderableReturnType(typeName + Nullable.Render(returnType));
    }
}
