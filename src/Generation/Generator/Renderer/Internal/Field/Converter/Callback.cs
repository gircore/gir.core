using System.Linq;

namespace Generator.Renderer.Internal.Field;

internal class Callback : FieldConverter
{
    public bool Supports(GirModel.Field field)
    {
        return field.AnyTypeOrCallback.IsT1;
    }

    public RenderableField Convert(GirModel.Field field)
    {
        return new RenderableField(
            Name: Model.Field.GetName(field),
            Attribute: null,
            NullableTypeName: RenderTypeName(field.AnyTypeOrCallback.AsT1)
        );
    }

    private static string RenderTypeName(GirModel.Callback callback)
    {
        var connect = callback.Parameters.Any() ? ", " : string.Empty;
        var parameters = CallbackParameters.RenderUnmanagedCallersTypeDefinition(callback.Parameters);
        var returnType = ReturnTypeRendererCallback.Render(callback.ReturnType);
        return $"delegate* unmanaged<{parameters}{connect}{returnType}>";
    }
}
