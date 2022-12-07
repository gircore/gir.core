namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal interface ToManagedParameterConverter
{
    bool Supports(GirModel.AnyType type);
    string? GetExpression(GirModel.Parameter parameter, out string variableName);
}
