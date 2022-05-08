using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class UnsignedPointerParameter
{
    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: string.Empty,
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    //IntPtr can't be nullable. They can be "nulled" via IntPtr.Zero.
    private static string GetNullableTypeName(GirModel.Parameter parameter)
        => Type.GetName(parameter.AnyType.AsT0);
}
