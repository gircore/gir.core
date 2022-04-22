using GirModel;

namespace Generator3.Converter.ReturnType.ToManaged;

internal class Pointer : ReturnTypeConverter
{
    public bool Supports(AnyType type)
        => type.Is<GirModel.Pointer>();

    public string GetString(GirModel.ReturnType returnType, string fromVariableName)
        => fromVariableName;
}
