namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PlatformStringArray : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.IsArray<GirModel.PlatformString>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        if (returnType.AnyType.AsT1.Length == null)
            return NullTerminatedArray(returnType, fromVariableName);
        else
            return SizeBasedArray(returnType, fromVariableName);
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
