using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class PrimitiveValueType : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.PrimitiveValueType>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
