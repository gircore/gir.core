using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class PointerParameter
{
    //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: Type.GetName(parameter.AnyType.AsT0),
            Name: Parameter.GetName(parameter)
        );
    }
}
