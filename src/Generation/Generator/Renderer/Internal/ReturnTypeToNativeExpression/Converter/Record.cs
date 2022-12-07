using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Record : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName + ".Handle";
}
