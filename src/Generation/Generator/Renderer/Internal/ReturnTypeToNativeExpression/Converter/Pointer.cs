using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Pointer : ReturnTypeConverter
{
    public bool Supports(AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.Pointer>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
