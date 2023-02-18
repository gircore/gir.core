using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PlatformString : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.PlatformString>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        // Convert the PlatformStringHandle return type to a string.
        return $"{fromVariableName}.ConvertToString()";
    }
}
