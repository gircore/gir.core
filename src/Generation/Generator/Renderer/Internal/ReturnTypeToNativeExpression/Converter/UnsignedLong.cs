using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class UnsignedLong : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.UnsignedLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
