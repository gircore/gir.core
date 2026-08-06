using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal interface ReturnTypeConverter
{
    bool Supports(AnyTypeReference anyTypeReference);
    string GetString(GirModel.ReturnType returnType, string fromVariableName);
}
