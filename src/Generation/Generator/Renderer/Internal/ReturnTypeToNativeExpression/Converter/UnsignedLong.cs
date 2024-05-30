namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class UnsigendLong : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.UnsignedLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => $"new CULong(checked((nuint){fromVariableName}))";
}
