using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class PrimitiveValueTypeParameter
{

    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter) => parameter switch
    {
        // Public bindings are not generated for pointer types with direction=in, so just
        // use a pointer for the internal binding.
        { Direction: GirModel.Direction.In, IsPointer: true } => Type.Pointer,
        _ => Type.GetName(parameter.AnyType.AsT0)
    };

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        // - Note the caller-allocates flag doesn't seem to be meaningful for primitive types and is inconsistently used.
        //   When this is false, native functions still just expect to have an integer pointer provided by the caller to write to.
        // - Nullable inout and out types are just exposed as non-nullable ref / out parameters which the user can ignore if desired.
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
