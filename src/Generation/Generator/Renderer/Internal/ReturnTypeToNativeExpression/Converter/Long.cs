using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Long : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Long>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
