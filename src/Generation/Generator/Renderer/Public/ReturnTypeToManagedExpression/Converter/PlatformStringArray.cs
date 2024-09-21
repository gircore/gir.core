using System;
using System.Collections.Generic;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PlatformStringArray : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PlatformString>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> _)
    {
        data.SetExpression(fromVariableName =>
        {
            var returnType = data.ReturnType;
            var arrayType = returnType.AnyType.AsT1;

            if (arrayType.IsZeroTerminated)
                return NullTerminatedArray(returnType, fromVariableName);

            if (arrayType.Length is not null)
                return SizeBasedArray(returnType, fromVariableName);

            throw new Exception("Unknown kind of array");
        });
    }

    private static string NullTerminatedArray(GirModel.ReturnType returnType, string fromVariableName)
    {
        return returnType.Nullable
            ? $"{fromVariableName}.ConvertToStringArray()"
            : $"{fromVariableName}.ConvertToStringArray() ?? throw new System.Exception(\"Unexpected null value\")";
    }

    private static string SizeBasedArray(GirModel.ReturnType returnType, string fromVariableName)
    {
        return fromVariableName;
    }
}
