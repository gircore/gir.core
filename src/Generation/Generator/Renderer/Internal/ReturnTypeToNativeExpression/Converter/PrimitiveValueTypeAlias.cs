using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class PrimitiveValueTypeAlias : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.IsAlias<GirModel.PrimitiveValueType>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
