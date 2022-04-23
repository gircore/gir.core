using System;
using GirModel;

namespace Generator3.Converter.Parameter.ToNative;

internal class PrimitiveValueType : ParameterConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.PrimitiveValueType>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        if (parameter.IsPointer)
            throw new NotImplementedException($"{parameter.AnyType}: Pointed primitive value types can not yet be converted to native");

        if (parameter.Direction != Direction.In)
            throw new NotImplementedException($"{parameter.AnyType}: Primitive value type with direction != in not yet supported");

        //We don't need any conversion for native parameters
        variableName = parameter.GetPublicName();
        return null;
    }
}
