using GirModel;

namespace Generator.Renderer.Internal.ReturnTypeToNativeExpressions;

internal class Record : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>(out var record) && Model.Record.IsStandard(record);

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName + ".Handle";
}
