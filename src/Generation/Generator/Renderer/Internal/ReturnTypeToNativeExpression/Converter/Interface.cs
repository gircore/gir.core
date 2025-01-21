namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Interface : ReturnTypeConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.Interface>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
    {
        return returnType.Nullable
            ? fromVariableName + "?.Handle.DangerousGetHandle() ?? IntPtr.Zero"
            : fromVariableName + ".Handle.DangerousGetHandle()";
    }
}
