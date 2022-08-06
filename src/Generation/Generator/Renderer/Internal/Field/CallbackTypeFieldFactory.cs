using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class CallbackTypeFieldFactory
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
        var type = (GirModel.Callback) field.AnyTypeOrCallback.AsT0.AsT0;
        return Namespace.GetInternalName(type.Namespace) + "." + Type.GetName((GirModel.Type) type);
    }
}
