namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class UnsignedCLong : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.UnsignedCLong>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => $"new CULong(checked((nuint){fromVariableName}))";
}
