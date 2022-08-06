using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class CallbackFieldFactory
{
    public static RenderableField Create(GirModel.Field field)
    {
        return new RenderableField(
            Name: Field.GetName(field),
            Attribute: null,
            NullableTypeName: Model.Callback.GetInternalDelegateName(field.AnyTypeOrCallback.AsT1)
        );
    }
}
