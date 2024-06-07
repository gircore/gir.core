using GirModel;

namespace Generator.Renderer.Public.ReturnTypeToManagedExpressions;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.UnsignedLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => returnType.IsPointer
            ? fromVariableName
            : $"{fromVariableName}.Value";
}
