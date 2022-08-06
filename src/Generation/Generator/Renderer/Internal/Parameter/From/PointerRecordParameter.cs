using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class PointerRecordParameter
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Type.Pointer,
            Name: Parameter.GetName(parameter)
        );
    }
}
