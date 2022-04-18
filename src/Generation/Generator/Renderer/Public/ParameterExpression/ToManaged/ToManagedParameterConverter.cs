using GirModel;

namespace Generator.Renderer.Public.ParameterExpressions;

internal interface ToManagedParameterConverter
{
    bool Supports(AnyType type);
    string? GetExpression(GirModel.Parameter parameter, out string variableName);
}
