using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Pointer : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Pointer>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
