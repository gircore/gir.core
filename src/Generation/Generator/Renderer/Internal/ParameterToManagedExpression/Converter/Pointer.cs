using System;
using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Pointer : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Pointer>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Pointer with direction != in not yet supported");

        //We don't need any conversion for bitfields
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
