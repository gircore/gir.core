using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class CallbackParameters
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
        new Parameter.RecordAsPointer(), //Callbacks do not support record safe handles in parameters
        
        new Parameter.ArrayClass(),
        new Parameter.ArrayInterface(),
        new Parameter.ArrayString(),
        new Parameter.ArrayEnumeration(),
        new Parameter.ArrayRecord(),
        new Parameter.ArrayByte(),
        new Parameter.ArrayNativeUnsignedInteger(),
        new Parameter.ArrayGLibPointer(),
        new Parameter.ArrayGLibPointerPointer(),
        new Parameter.ArrayGLibPointerPrimitiveValueType(),
        new Parameter.ArrayGLibPointerRecord(),
        new Parameter.ArrayGLibByte(),
        new Parameter.ArrayGLibPrimitiveValueType(),
        new Parameter.ArrayPointer(),
        new Parameter.ArrayPrimitiveValueType(),
    };

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
