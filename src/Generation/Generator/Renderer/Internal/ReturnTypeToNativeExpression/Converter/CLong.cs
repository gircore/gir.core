namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class CLong : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyTypeReference anyTypeReference)
        => anyTypeReference.References<GirModel.CLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => $"new CLong(checked((nint){fromVariableName}))";
}
