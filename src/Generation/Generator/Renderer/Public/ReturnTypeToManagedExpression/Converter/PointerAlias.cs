using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class PointerAlias : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.IsAlias<GirModel.Pointer>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
