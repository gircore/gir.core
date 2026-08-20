using Generator.Model;

namespace Generator.Renderer.Public.ReturnType;

internal class PointerAlias : ReturnTypeConverter
{
    public RenderableReturnType Create(GirModel.ReturnType returnType)
    {
        var alias = (GirModel.Alias) returnType.AnyTypeReference.AsT0.Type;
        var nullableTypeName = $"{Namespace.GetPublicName(alias.Namespace)}.{Type.GetName(alias)}";

        return new RenderableReturnType(nullableTypeName);
    }

    public bool Supports(GirModel.ReturnType returnType)
        => returnType.AnyTypeReference.ReferencesAlias<GirModel.Pointer>();
}
