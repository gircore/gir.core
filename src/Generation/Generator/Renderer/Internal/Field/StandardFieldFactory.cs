using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class StandardFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: Field.GetName(field),
            Attribute: null,
            NullableTypeName: GetNullableTypeName(field)
        );
    }

    private static string GetNullableTypeName(GirModel.Field field)
    {
        return field.IsPointer
            ? Type.Pointer
            : Type.GetName(field.AnyTypeOrCallback.AsT0.AsT0);
    }
}
