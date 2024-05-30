using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class Long : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Long>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => returnType.IsPointer
            ? fromVariableName
            : $"{fromVariableName}.Value";
}
