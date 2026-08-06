using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class PrimitiveValueTypeAlias : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.ReferencesAlias<GirModel.PrimitiveValueType>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
