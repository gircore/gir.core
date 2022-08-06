using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class EnumerationFieldFactory
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
        var type = (GirModel.Enumeration) field.AnyTypeOrCallback.AsT0.AsT0;
        return field.IsPointer
            ? Type.Pointer
            : ComplexType.GetFullyQualified(type);
    }
}
