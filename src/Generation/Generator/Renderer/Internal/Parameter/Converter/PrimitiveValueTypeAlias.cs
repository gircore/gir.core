namespace Generator.Renderer.Internal.Parameter;

internal class PrimitiveValueTypeAlias : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.IsAlias<GirModel.PrimitiveValueType>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        // If the parameter is both nullable and optional this implies a parameter type like 'int **' and possibly
        // ownership transfer (this combination does not currently occur for any functions).
        if (parameter is { Nullable: true, Optional: true })
            throw new System.NotImplementedException($"{parameter.AnyTypeOrVarArgs} - Primitive value type with nullable=true and optional=true not yet supported");

        // Nullable-only parameters likely have incorrect annotations and should be marked optional instead.
        if (parameter.Nullable)
            Log.Information($"Primitive value type '{parameter.Name}' with nullable=true is likely an incorrect annotation");

        // The caller-allocates flag is not meaningful for primitive types (https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/446)
        if (parameter.CallerAllocates)
            Log.Information($"Primitive value type '{parameter.Name}' with caller-allocates=true is an incorrect annotation");

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: Model.Type.GetPublicNameFullyQuallified(parameter.AnyTypeOrVarArgs.AsT0.AsT0),
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        // - Optional inout and out types are just exposed as non-nullable ref / out parameters which the user can ignore if desired.
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        { Direction: GirModel.Direction.In, IsPointer: true } => ParameterDirection.Ref(),
        _ => ParameterDirection.In()
    };
}
