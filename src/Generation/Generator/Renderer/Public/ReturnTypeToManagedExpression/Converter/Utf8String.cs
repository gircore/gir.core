using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Utf8String : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Utf8String>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        // Convert the Utf8StringHandle return type to a string.
        return $"{fromVariableName}.ConvertToString()";
    }
}
