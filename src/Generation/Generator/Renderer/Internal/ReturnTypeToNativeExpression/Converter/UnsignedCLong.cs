namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class UnsignedCLong : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.UnsignedCLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => $"new CULong(checked((nuint){fromVariableName}))";
}
