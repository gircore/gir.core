using Generator.Model;

namespace Generator.Renderer.Internal;

internal static class PrimitiveValueTypeParameter
{

    public static RenderableParameter Create(GirModel.Parameter parameter)
    {
        // If the parameter is both nullable and optional this implies a parameter type like 'int **' and possibly
        // ownership transfer (this combination does not currently occur for any functions).
        if (parameter.Nullable && parameter.Optional)
            throw new System.NotImplementedException($"{parameter.AnyType} - Primitive value type with nullable=true and optional=true not yet supported");

        // Nullable-only parameters likely have incorrect annotations and should be marked optional instead.
        if (parameter.Nullable)
            Log.Information($"Primitive value type '{parameter.Name}' with nullable=true is likely an incorrect annotation");

        // The caller-allocates flag is not meaningful for primitive types (https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/446)
        if (parameter.CallerAllocates)
            Log.Information($"Primitive value type '{parameter.Name}' with caller-allocates=true is an incorrect annotation");

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: GetNullableTypeName(parameter),
            Name: Parameter.GetName(parameter)
        );
    }

    private static string GetNullableTypeName(GirModel.Parameter parameter) => parameter switch
    {
        // Public bindings are not generated for pointer types with direction=in, but we can still generate the internal binding with a pointer.
        { Direction: GirModel.Direction.In, IsPointer: true } => Type.Pointer,
        _ => Type.GetName(parameter.AnyType.AsT0)
    };

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        // - Optional inout and out types are just exposed as non-nullable ref / out parameters which the user can ignore if desired.
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        _ => ParameterDirection.In()
    };
}
