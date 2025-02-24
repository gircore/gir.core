using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.UnsignedLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
