using System;
using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class Bitfield : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Bitfield>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Bitfield with direction != in not yet supported");

        //We don't need any conversion for bitfields
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
