using System;
using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Enumeration : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Enumeration>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyTypeOrVarArgs}: Enumeration with direction != in not yet supported");

        //We don't need any conversion for enumerations
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
