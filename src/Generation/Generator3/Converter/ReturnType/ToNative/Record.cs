using GirModel;

namespace Generator3.Converter.ReturnType.ToNative;

internal class Record : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Record>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName + ".Handle";
}
