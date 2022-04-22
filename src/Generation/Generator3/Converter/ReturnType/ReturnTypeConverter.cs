using GirModel;

namespace Generator3.Converter;

internal interface ReturnTypeConverter
{
    bool Supports(AnyType type);
    string GetString(GirModel.ReturnType returnType, string fromVariableName);
}
