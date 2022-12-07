using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal interface ReturnTypeConverter
{
    bool Supports(AnyType type);
    string GetString(GirModel.ReturnType returnType, string fromVariableName);
}
