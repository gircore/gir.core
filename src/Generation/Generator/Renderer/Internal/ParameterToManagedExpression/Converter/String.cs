using Generator.Model;

namespace Generator.Renderer.Internal.ParameterToManagedExpressions;

internal class String : ToManagedParameterConverter
{
    public bool Supports(GirModel.AnyType type)
        => type.Is<GirModel.String>();

    public string? GetExpression(GirModel.Parameter parameter, out string variableName)
    {
        variableName = Parameter.GetName(parameter);
        return null;
    }
}
