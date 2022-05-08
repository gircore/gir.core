using System;
using Generator.Model;

namespace Generator.Renderer.Public.ParameterExpressions.ToManaged;

internal class PrimitiveValueType : ParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.PrimitiveValueType>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Pointed primitive value types can not yet be converted to managed");

        if (parameter.Direction != GirModel.Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Primitive value type with direction != in not yet supported");

        //We don't need any conversion for native parameters
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
