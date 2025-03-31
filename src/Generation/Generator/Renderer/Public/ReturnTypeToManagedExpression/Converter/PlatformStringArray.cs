using System;
using System.Collections.Generic;
using System.Linq;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PlatformStringArray : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PlatformString>();

    public void Initialize(ReturnTypeToManagedData data, IEnumerable<ParameterToNativeData> parametersToNativeData)
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
        
        data.SetPostReturnStatement(fromvariableName =>
        {
            var returnType = data.ReturnType;
            var arrayType = returnType.AnyType.AsT1;

            if (arrayType.IsZeroTerminated)
                return null;

            if (arrayType.Length is not null)
            {
                var sizeParameter = parametersToNativeData.ElementAt(arrayType.Length.Value);
                return $"{fromvariableName}.Size = {sizeParameter.GetCallName()};";
            }

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
        return returnType.Nullable
            ? $"{fromVariableName}.ConvertToStringArray()"
            : $"{fromVariableName}.ConvertToStringArray() ?? throw new System.Exception(\"Unexpected null value\")";
    }
}
