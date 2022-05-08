using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class StringFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: Field.GetName(field),
            Attribute: MarshalAs.UnmanagedLpString(),
            NullableTypeName: Type.GetName(field.AnyTypeOrCallback.AsT0.AsT0)
        );
    }
}
