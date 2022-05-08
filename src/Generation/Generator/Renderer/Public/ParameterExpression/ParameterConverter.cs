using GirModel;

namespace Generator.Renderer.Public.ParameterExpressions;

internal interface ParameterConverter
{
    bool Supports(AnyType type);
    string? GetExpression(GirModel.Parameter parameter, out string variableName);
}
