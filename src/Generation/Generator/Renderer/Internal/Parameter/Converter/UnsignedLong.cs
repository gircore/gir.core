using System;

namespace Generator.Renderer.Internal.Parameter;

internal class UnsignedLong : ParameterConverter
{
    public bool Supports(GirModel.AnyType anyType)
    {
        return anyType.Is<GirModel.UnsignedLong>();
    }

    public RenderableParameter Convert(GirModel.Parameter parameter)
    {
        // If the parameter is both nullable and optional this implies a parameter type like 'int **' and possibly
        // ownership transfer (this combination does not currently occur for any functions).
        if (parameter is { Nullable: true, Optional: true })
            throw new System.NotImplementedException($"{parameter.AnyTypeOrVarArgs} - Unsigned long value type with nullable=true and optional=true not yet supported");

        // Nullable-only parameters likely have incorrect annotations and should be marked optional instead.
        if (parameter.Nullable)
            Log.Information($"Unsigned long value type '{parameter.Name}' with nullable=true is likely an incorrect annotation");

        // The caller-allocates flag is not meaningful for primitive types (https://gitlab.gnome.org/GNOME/gobject-introspection/-/issues/446)
        if (parameter.CallerAllocates)
            Log.Information($"Unsigned long value type '{parameter.Name}' with caller-allocates=true is an incorrect annotation");

        return new RenderableParameter(
            Attribute: string.Empty,
            Direction: GetDirection(parameter),
            NullableTypeName: "CULong",
            Name: Model.Parameter.GetName(parameter)
        );
    }

    private static string GetDirection(GirModel.Parameter parameter) => parameter switch
    {
        // - Optional inout and out types are just exposed as non-nullable ref / out parameters which the user can ignore if desired.
        { Direction: GirModel.Direction.In, IsPointer: true } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.InOut } => ParameterDirection.Ref(),
        { Direction: GirModel.Direction.Out } => ParameterDirection.Out(),
        { Direction: GirModel.Direction.In } => ParameterDirection.In(),
        _ => throw new Exception($"Can't figure out direction for internal unsigned long value type parameter {parameter}.")
    };
}
