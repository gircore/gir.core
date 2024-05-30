namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Long : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Long>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => $"new CLong(checked((nint){fromVariableName}))";
}
