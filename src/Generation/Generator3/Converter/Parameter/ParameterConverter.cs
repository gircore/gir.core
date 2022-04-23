using GirModel;

namespace Generator3.Converter;

internal interface ParameterConverter
{
    bool Supports(AnyType type);
    string? GetExpression(GirModel.Parameter parameter, out string variableName);
}
