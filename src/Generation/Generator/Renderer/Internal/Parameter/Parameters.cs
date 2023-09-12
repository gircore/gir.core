using System.Collections.Generic;

namespace Generator.Renderer.Internal;

internal static class Parameters
{
    private static readonly List<Parameter.ParameterConverter> converters = new()
    {
        new Parameter.Bitfield(),
        new Parameter.Callback(),
        new Parameter.CallbackAlias(),
        new Parameter.Class(),
        new Parameter.ClassArray(),
        new Parameter.ClassGLibPtrArray(),
        new Parameter.Enumeration(),
        new Parameter.EnumerationArray(),
        new Parameter.GLibByteArray(),
        new Parameter.Interface(),
        new Parameter.InterfaceArray(),
        new Parameter.NativeUnsignedIntegerArray(),
        new Parameter.OpaqueTypedRecord(),
        new Parameter.OpaqueTypedRecordArray(),
        new Parameter.PlatformStringArray(),
        new Parameter.Pointer(),
        new Parameter.PointerAlias(),
        new Parameter.PointerArray(),
        new Parameter.PointerGLibArray(),
        new Parameter.PointerGLibPtrArray(),
        new Parameter.PrimitiveValueType(),
        new Parameter.PrimitiveValueTypeAlias(),
        new Parameter.PrimitiveValueTypeArray(),
        new Parameter.PrimitiveValueTypeArrayAlias(),
        new Parameter.PrimitiveValueTypeGLibArray(),
        new Parameter.PrimitiveValueTypeGLibArrayAlias(),
        new Parameter.PrimitiveValueTypeGLibPtrArray(),
        new Parameter.Record(),
        new Parameter.RecordAlias(),
        new Parameter.RecordArray(),
        new Parameter.RecordGLibPtrArray(),
        new Parameter.String(),
        new Parameter.StringGLibPtrArray(),
        new Parameter.Union(),
        new Parameter.UnsignedPointer(),
        new Parameter.Utf8StringArray(),
        new Parameter.Void(),
    };

    public static string GetNullableTypeName(GirModel.Parameter parameter)
    {
        if (parameter.AnyTypeOrVarArgs.IsT1)
            throw new System.Exception($"Parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered as variadic parameters are not supported");

        foreach (var converter in converters)
            if (converter.Supports(parameter.AnyTypeOrVarArgs.AsT0))
                return converter.Convert(parameter).NullableTypeName;

        throw new System.Exception($"Internal parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered");
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

        throw new System.Exception($"Internal parameter \"{parameter.Name}\" of type {parameter.AnyTypeOrVarArgs} can not be rendered");
    }

    private static string Render(Parameter.RenderableParameter parameter)
        => $@"{parameter.Attribute}{parameter.Direction}{parameter.NullableTypeName} {parameter.Name}";
}
