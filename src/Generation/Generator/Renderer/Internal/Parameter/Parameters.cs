using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class Parameters
{
    private static readonly List<Parameter.ParameterConverter> converters = new()
    {
        new Parameter.String(),
        new Parameter.Pointer(),
        new Parameter.UnsignedPointer(),
        new Parameter.Class(),
        new Parameter.Interface(),
        new Parameter.Union(),
        new Parameter.PrimitiveValueType(),
        new Parameter.Callback(),
        new Parameter.Enumeration(),
        new Parameter.Bitfield(),
        new Parameter.Void(),
        new Parameter.Record(),

        new Parameter.ClassArray(),
        new Parameter.InterfaceArray(),
        new Parameter.StringArray(),
        new Parameter.EnumerationArray(),
        new Parameter.RecordArray(),
        new Parameter.ByteArray(),
        new Parameter.NativeUnsignedIntegerArray(),
        new Parameter.PointerGLibArray(),
        new Parameter.PointerGLibPtrArray(),
        new Parameter.PrimitiveValueTypeGLibPtrArray(),
        new Parameter.ArrayGLibPointerRecord(),
        new Parameter.GLibByteArray(),
        new Parameter.PrimitiveValueTypeGLibArray(),
        new Parameter.PointerArray(),
        new Parameter.PrimitiveValueTypeArray(),
    };

    public static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.IsT1)
            throw new System.Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered as variadic parameters are not supported");

        foreach (var converter in converters)
            if (converter.Supports(parameter.AnyTypeOrVarArgs.AsT0))
                return converter.Convert(parameter).NullableTypeName;

        throw new System.Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered");
    }

    public static string Render(IEnumerable<GirModel.Parameter> parameters)
    {
        var parameterList = new List<string>();

        foreach (var parameter in parameters)
            parameterList.Add(Render(parameter));

        return parameterList.Join(", ");
    }

    private static string Render(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.IsT1)
            throw new System.Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered as variadic parameters are not supported");

        foreach (var converter in converters)
            if (converter.Supports(parameter.AnyTypeOrVarArgs.AsT0))
                return Render(converter.Convert(parameter));

        throw new System.Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered");
    }

    private static string Render(Parameter.RenderableParameter parameter)
        => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
}
